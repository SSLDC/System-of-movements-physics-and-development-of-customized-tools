using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{
    // Referencias a otros scripts y animator
    public Player movement;      // Script que controla el movimiento
    public PlayerActions actions; // Script que controla las acciones del jugador
    public Animator animator;    // Animator del personaje

    void Awake()
    {
        // Inicializamos referencias
        movement = GetComponent<Player>();
        actions = FindAnyObjectByType<PlayerActions>();
        animator = gameObject.GetComponent<Animator>();
    }

    void Start()
    {
        // Vacío, no se usa por ahora
    }

    void Update()
    {
        // Llamamos a cada animación según estado
        MoveAnim();
        JumpAnim();
        DashAnim();
        CrounchedAnim();

        // Aquí podrían ir más animaciones de acciones
    }

    // --- Animación de movimiento ---
    public void MoveAnim()
    {
        // Corre si se mueve horizontalmente, está en suelo, no agachado ni dash, y no apunta
        if (Mathf.Abs(movement.MovePlayer) > 0.01f && 
            !movement.isDuck && 
            !movement.isDashing && 
            movement.IsGrounded && 
            !actions.isAim && 
            !actions.isAimCrounched)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    // --- Animación de salto y caída ---
    public void JumpAnim()
    {
        if (movement.isDashing) return; // no saltar mientras dash

        if (movement.rb.linearVelocity.y > 0.1f && !movement.IsGrounded)
        {
            animator.SetBool("Jump", true);
            animator.SetBool("Fall", false);
        }
        else if (movement.rb.linearVelocity.y < -0.1f && !movement.IsGrounded)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", true);
        }
        else if (movement.IsGrounded)
        {
            animator.SetBool("Jump", false);
            animator.SetBool("Fall", false);
        }
    }

    // --- Animación de agachado ---
    public void CrounchedAnim()
    {
        animator.SetBool("Crounched", movement.isDuck && !movement.isDashing);
    }

    // --- Animación de dash ---
    public void DashAnim()
    {
        animator.SetBool("Dash", movement.isDashing);
    }

    // --- Animación de ataque ---
    public void AttackAnim()
    {
        animator.SetBool("Attack", true);
    }

    // --- Final de animación de ataque ---
    public void OnAttackEnd()
    {
        animator.SetBool("Attack", false);
        actions.isAttacking = false;

        // Failsafe: si algo falla, se asegura que termine el ataque
        StartCoroutine(AttackFailSafe(1f));
    }

    // --- Animación de apuntar ---
    public void AimAnim()
    {
        animator.SetBool("Aim", actions.isAim);
    }

    // --- Animación de apuntar agachado ---
    public void AimCrounchedAnim()
    {
        animator.SetBool("AimCrounched", actions.isAimCrounched);
    }

    // --- Failsafe para ataques que no terminan ---
    IEnumerator AttackFailSafe(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (actions.isAttacking)
        {
            OnAttackEnd();
        }
    }
}