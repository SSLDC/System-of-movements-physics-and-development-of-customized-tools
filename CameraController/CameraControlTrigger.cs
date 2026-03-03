using Unity.Cinemachine;
using UnityEngine;
using UnityEditor;

using UnityEngine;
using UnityEditor;

// --- Control de cámara usando triggers ---
public class CameraControlTrigger : MonoBehaviour
{
    public CustomInspectorObjects customInspectorObjects; // Configuración de la cámara editable en el inspector

    private Collider2D _coll; // Referencia al collider del trigger

    private void Start()
    {
        _coll = GetComponent<Collider2D>(); // Guardamos el collider para usar después
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Solo actuamos si el jugador entra al trigger
        if (collision.CompareTag("Player"))
        {
            // Si la opción de pan está activa, movemos la cámara
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(
                    customInspectorObjects.panDistance,
                    customInspectorObjects.panTime,
                    customInspectorObjects.panDirection,
                    false // false = entró al trigger
                );
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Solo actuamos si el jugador sale del trigger
        if (collision.CompareTag("Player"))
        {
            // Calculamos dirección de salida relativa al centro del trigger
            Vector2 exitDirection = (collision.transform.position - _coll.bounds.center).normalized;

            // Si está activo swap de cámaras, cambiamos dependiendo de la dirección
            if (customInspectorObjects.swapCameras &&
                customInspectorObjects.cameraOnLeft != null &&
                customInspectorObjects.cameraOnRight != null)
            {
                CameraManager.instance.SwapCamera(
                    customInspectorObjects.cameraOnLeft,
                    customInspectorObjects.cameraOnRight,
                    exitDirection
                );
            }

            // Reiniciamos el pan de cámara al salir del trigger
            if (customInspectorObjects.panCameraOnContact)
            {
                CameraManager.instance.PanCameraOnContact(
                    customInspectorObjects.panDistance,
                    customInspectorObjects.panTime,
                    customInspectorObjects.panDirection,
                    true // true = salió del trigger
                );
            }
        }
    }
}

// --- Clase que contiene todas las opciones visibles en inspector ---
[System.Serializable]
public class CustomInspectorObjects
{
    public bool swapCameras = false;        // Activar cambio de cámara al salir
    public bool panCameraOnContact = false; // Activar pan al entrar o salir

    [HideInInspector] public CinemachineCamera cameraOnLeft;  // Cámara izquierda (si swap)
    [HideInInspector] public CinemachineCamera cameraOnRight; // Cámara derecha (si swap)

    [HideInInspector] public PanDirection panDirection; // Dirección del pan
    [HideInInspector] public float panDistance = 3f;    // Distancia del pan
    [HideInInspector] public float panTime = 0.35f;     // Tiempo del pan
}

// --- Enum con direcciones posibles de pan ---
public enum PanDirection
{
    Up,
    Down,
    Left,
    Right
}

// --- Editor personalizado para mostrar solo las opciones necesarias ---
[CustomEditor(typeof(CameraControlTrigger))]
public class MyScriptEditor : Editor
{
    CameraControlTrigger cameraControlTrigger;

    private void OnEnable()
    {
        cameraControlTrigger = (CameraControlTrigger)target;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Dibuja los campos básicos

        // Mostrar referencias de cámaras solo si swap está activo
        if (cameraControlTrigger.customInspectorObjects.swapCameras)
        {
            cameraControlTrigger.customInspectorObjects.cameraOnLeft =
                EditorGUILayout.ObjectField("Camera on Left",
                cameraControlTrigger.customInspectorObjects.cameraOnLeft,
                typeof(CinemachineCamera), true) as CinemachineCamera;

            cameraControlTrigger.customInspectorObjects.cameraOnRight =
                EditorGUILayout.ObjectField("Camera on Right",
                cameraControlTrigger.customInspectorObjects.cameraOnRight,
                typeof(CinemachineCamera), true) as CinemachineCamera;
        }

        // Mostrar opciones de pan solo si pan está activo
        if (cameraControlTrigger.customInspectorObjects.panCameraOnContact)
        {
            cameraControlTrigger.customInspectorObjects.panDirection =
                (PanDirection)EditorGUILayout.EnumPopup("Camera Pan Direction",
                cameraControlTrigger.customInspectorObjects.panDirection);

            cameraControlTrigger.customInspectorObjects.panDistance =
                EditorGUILayout.FloatField("Pan Distance",
                cameraControlTrigger.customInspectorObjects.panDistance);

            cameraControlTrigger.customInspectorObjects.panTime =
                EditorGUILayout.FloatField("Pan Time",
                cameraControlTrigger.customInspectorObjects.panTime);
        }

        // Marca el objeto como modificado si cambió algo en el inspector
        if (GUI.changed)
        {
            EditorUtility.SetDirty(cameraControlTrigger);
        }
    }
}