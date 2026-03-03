using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

using UnityEngine;
using System.Collections;

// --- Control principal de cámaras virtuales con Cinemachine ---
public class CameraManager : MonoBehaviour
{
    public static CameraManager instance; // Singleton para acceso global

    [SerializeField] private CinemachineCamera[] _allVirtualCameras; // Todas las cámaras virtuales

    [Header("Controles para el Y damping cuando el jugador salta o cae")]
    [SerializeField] private float _fallPanAmount = 0.25f;   // Damping al caer
    [SerializeField] private float _fallYPanTime = 0.35f;    // Tiempo para interpolar damping
    public float _fallSpeedYDampingChangeThreshold = -15f;   // Velocidad de caída para activar damping

    public bool IsLerpingYDamping { get; private set; }      // Si se está haciendo Lerp en Y
    public bool LerpedFromPlayerFalling { get; set; }        // Marca si se aplicó damping por caída

    private Coroutine _lerpYPanCoroutine; // Coroutine de Lerp
    private Coroutine _panCameraCoroutine; // Coroutine de pan
    private CinemachineCamera _currentCamera; // Cámara activa
    private CinemachinePositionComposer _framingTransposer; // Componente para offset y damping

    private float _nomYPanAmount; // Damping Y por defecto
    private Vector2 _startingTrackedObjectOffset; // Offset de cámara por defecto

    // --- Inicialización ---
    private void Awake()
    {
        // Setup singleton
        if (instance == null)
            instance = this;

        // Busca la cámara activa al inicio
        for (int i = 0; i < _allVirtualCameras.Length; i++)
        {
            if (_allVirtualCameras[i].enabled)
            {
                _currentCamera = _allVirtualCameras[i];
                _framingTransposer = _currentCamera.GetComponent<CinemachinePositionComposer>();
            }
        }

        // Guarda valores por defecto
        _nomYPanAmount = _framingTransposer.Damping.y;
        _startingTrackedObjectOffset = _framingTransposer.TargetOffset;
    }

    #region Pan Camera
    // Inicia un movimiento de cámara (pan) hacia alguna dirección
    public void PanCameraOnContact(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        _panCameraCoroutine = StartCoroutine(PanCamera(panDistance, panTime, panDirection, panToStartingPos));
    }

    private IEnumerator PanCamera(float panDistance, float panTime, PanDirection panDirection, bool panToStartingPos)
    {
        Vector2 endPos = Vector2.zero;
        Vector2 startingPos;

        if (!panToStartingPos)
        {
            // Determina dirección del pan
            switch (panDirection)
            {
                case PanDirection.Up: endPos = Vector2.up; break;
                case PanDirection.Down: endPos = Vector2.down; break;
                case PanDirection.Left: endPos = Vector2.left; break;
                case PanDirection.Right: endPos = Vector2.right; break;
            }

            endPos *= panDistance;
            startingPos = _startingTrackedObjectOffset;
            endPos += startingPos;
        }
        else
        {
            // Retorna al offset original
            startingPos = _framingTransposer.TargetOffset;
            endPos = _startingTrackedObjectOffset;
        }

        // Interpolación suave entre posiciones
        float elapsedTime = 0f;
        while (elapsedTime < panTime)
        {
            elapsedTime += Time.deltaTime;
            Vector3 panLerp = Vector3.Lerp(startingPos, endPos, (elapsedTime / panTime));
            _framingTransposer.TargetOffset = panLerp;
            yield return null;
        }
    }
    #endregion

    #region Swap Cameras
    // Cambia entre cámaras izquierda/derecha según dirección de salida del trigger
    public void SwapCamera(CinemachineCamera cameraFromLeft, CinemachineCamera cameraFromRight, Vector2 triggerExitDirection)
    {
        if (_currentCamera == cameraFromLeft && triggerExitDirection.x > 0f)
        {
            cameraFromRight.enabled = true;
            cameraFromLeft.enabled = false;

            _currentCamera = cameraFromRight;
            _framingTransposer = _currentCamera.GetComponent<CinemachinePositionComposer>();
        }
        else if (_currentCamera == cameraFromRight && triggerExitDirection.x < 0f)
        {
            cameraFromLeft.enabled = true;
            cameraFromRight.enabled = false;

            _currentCamera = cameraFromLeft;
            _framingTransposer = _currentCamera.GetComponent<CinemachinePositionComposer>();
        }
    }
    #endregion

    #region Lerp Y Damping
    // Llama al Lerp de damping Y según caída del jugador
    public void LerpYDamping(bool isPlayerFalling)
    {
        _lerpYPanCoroutine = StartCoroutine(LerpYAction(isPlayerFalling));
    }

    private IEnumerator LerpYAction(bool isPlayerFalling)
    {
        IsLerpingYDamping = true;

        float startDampAmount = _framingTransposer.Damping.y;
        float endDampAmount;

        if (isPlayerFalling)
        {
            endDampAmount = _fallPanAmount; // Damping al caer
            LerpedFromPlayerFalling = true;
        }
        else
        {
            endDampAmount = _nomYPanAmount; // Damping por defecto
            LerpedFromPlayerFalling = false;
        }

        // Interpolación suave de damping
        float elapsedTime = 0f;
        while (elapsedTime < _fallYPanTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedPanAmount = Mathf.Lerp(startDampAmount, endDampAmount, (elapsedTime / _fallYPanTime));
            _framingTransposer.Damping.y = lerpedPanAmount;
            yield return null;
        }

        IsLerpingYDamping = false;
    }
    #endregion
}