using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using System.Collections;

// --- Objeto que sigue al jugador y rota suavemente cuando cambia de dirección ---
public class CameraFollowObject : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform _PlayerTransform; // Transform del jugador para seguirlo

    [Header("Flip Rotation Stats")]
    [SerializeField] private float _flipYRotationTime = 0.5f; // Tiempo para girar suavemente en Y

    private Coroutine _turnCoroutine; // Coroutine de rotación
    private bool _isFacingRight;      // Guarda hacia dónde mira el jugador
    private Player _player;            // Referencia al script Player

    // --- Inicialización ---
    public void Awake()
    {
        _player = _PlayerTransform.gameObject.GetComponent<Player>(); // Consigue el script Player
        _isFacingRight = _player.isFacingRight;                        // Detecta dirección inicial
    }

    private void Update()
    {
        // Hace que este objeto siga la posición del jugador
        transform.position = _player.transform.position;
    }

    // --- Llamado desde Player cuando cambia de dirección ---
    public void CallTurn(bool isFacingRight)
    {
        StopAllCoroutines(); // Detiene cualquier flip anterior
        _turnCoroutine = StartCoroutine(FlipYLerp(isFacingRight)); // Inicia la interpolación
    }

    // --- Interpolación suave de rotación en Y ---
    private IEnumerator FlipYLerp(bool isFacingRight)
    {
        float startRotation = transform.localEulerAngles.y;       // Rotación inicial
        float endRotationAmount = isFacingRight ? 0f : 180f;      // Rotación final según dirección
        float elapsedTime = 0f;

        // Lerp para rotar suavemente
        while (elapsedTime < _flipYRotationTime)
        {
            elapsedTime += Time.deltaTime;
            float yRotation = Mathf.Lerp(startRotation, endRotationAmount, elapsedTime / _flipYRotationTime);
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }
    }

    // --- Función extra para cambiar la dirección interna y devolver la rotación ---
    private float DetermineEndRotation()
    {
        _isFacingRight = !_isFacingRight; // Cambia la dirección

        if (_isFacingRight)
            return 180f; // Si mira a la derecha
        else
            return 0f;   // Si mira a la izquierda
    }
}