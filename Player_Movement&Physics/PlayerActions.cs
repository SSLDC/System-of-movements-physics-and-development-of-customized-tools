using UnityEngine;
using UnityEngine.Animations;

using UnityEngine;

// --- Control de acciones del jugador ---
public class PlayerActions : MonoBehaviour
{
    public Player movement;          // Referencia al script de movimiento
    public PlayerAnimation animations; // Referencia a animaciones

    [Header("Attack")]
    public bool isAttacking;         // Estado de ataque activo

    [Header("Shot")]
    public bool isAim;               // Apuntando con mouse
    public bool isAimCrounched;     // Apuntando mientras está agachado

    // --- Inicialización ---
    private void Awake()
    {
        movement = GetComponent<Player>();               // Busca script de movimiento
        animations = FindAnyObjectByType<PlayerAnimation>(); // Encuentra script de animaciones
    }

    // --- Update: se ejecuta cada frame ---
    void Update()
    {
        AttackMovement(); // Control de ataque según movimiento
        Aim();            // Control de apuntado
    }

    // --- Determina si puede atacar según movimiento ---
    public void AttackMovement()
    {
        // Si está quieto, bloquea movimiento y permite ataque
        if (movement.MovePlayer == 0)
        {
            movement.UseControls = false;
            Attack();
        }
        // Si se mueve y está en el aire, también puede atacar
        else if (movement.MovePlayer != 0 && !movement.IsGrounded)
        {
            Attack();
        }
    }

    // --- Detecta input de ataque ---
    public void Attack()
    {
        if (isAttacking) // Evita iniciar ataque si ya está atacando
            return;

        if (Input.GetKeyDown(KeyCode.Space)) // Presiona espacio para atacar
        {
            StartAttack();
        }
    }

    // --- Inicia ataque ---
    void StartAttack()
    {
        isAttacking = true;        // Marca que está atacando
        animations.AttackAnim();   // Llama a animación de ataque
    }

    // --- Control de apuntado con mouse ---
    public void Aim()
    {
        if (Input.GetMouseButton(1)) // Botón derecho presionado
        {
            isAim = true;
            animations.AimAnim();    // Activar animación de apuntar
        }
        else if (Input.GetMouseButtonUp(1)) // Botón derecho soltado
        {
            isAim = false;
            animations.AimAnim();    // Desactivar animación de apuntar
        }
    }
}