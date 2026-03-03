using System;
using UnityEngine;
using UnityEngine.InputSystem;

using UnityEngine;
using UnityEngine.InputSystem;

// --- Control principal del jugador ---
public class Player : MonoBehaviour
{
    [Header("Movimiento")]
    public InputActionReference ControlPlayer; // Referencia del input (Nuevo Input System)
    public float MovePlayer;                   // Valor horizontal de movimiento
    public float JumpForce;                    // Fuerza de salto base
    public float SpeedFall;                    // Fuerza extra al caer
    public Rigidbody2D rb;                     // Referencia al Rigidbody
    public float jumpInput;                    // Valor vertical de input

    public bool isFacingRight { get; private set; } // Para saber hacia dónde mira
    public float timeControls = 0f;                 // Contador de controles
    public bool UseControls;
    private float lastMoveX;                        // Último movimiento horizontal
    public PlayerActions actions;                   // Referencia a las acciones del jugador

    // Solo puede moverse si no está dash, atacando o apuntando
    public bool CanMove => !isDashing && !actions.isAttacking && !actions.isAim;

    [Header("Límites")]
    public float speed = 5f; // Velocidad de movimiento

    [Header("Agacharse")]
    public BoxCollider2D bc;             // Collider para agacharse
    private float originalSizeY;         // Tamaño original del collider Y
    private float originalOffsetY;       // Offset original del collider
    public bool isDuck { get; private set; } // Saber si está agachado

    [Header("Físicas")]
    public LayerMask GroundLayer;        // Capa de suelo
    public float RayLenght;              // Largo de rayos
    public int RayCount;                 // Cantidad de rayos
    public bool IsGrounded { get; private set; } // Está tocando suelo
    public float GravityScale;           // Gravedad normal
    public float FallJump;               // Fuerza extra al caer

    [Header("Control de salto adicional")]
    private float variableJumpTime = 0.25f; // Tiempo de salto variable
    private float jumpTimeCounter;           // Contador de tiempo de salto
    public bool isJumping { get; private set; } // Estado de salto

    [Header("Colisión lateral")]
    public LayerMask WallLayer;
    public float wallRayLength = 0.1f;
    public int horizontalRayCount = 9;
    public bool isTouchingWallLeft { get; private set; }
    public bool isTouchingWallRight { get; private set; }

    [Header("Colisión Plataforma")]
    public LayerMask PlatformLayer;
    public LayerMask PlatformIncline;
    public bool isTouchingPlatformLeft { get; private set; }
    public bool isTouchingPlatformRight { get; private set; }
    public bool isTouchingPlatformUp { get; private set; }

    [Header("Corner Correction")]
    public LayerMask CornerCorrectionLayer;
    public float rayDistance = 0.2f;
    public float cornerPushAmount = 0.7f;
    private bool raycastLeft;
    private bool raycastRight;

    [Header("Dash")]
    public bool isDashing { get; private set; } // Está haciendo dash
    public float DashDuration = 0.2f;           // Duración del dash
    public float DashTimeLeft;                  // Tiempo restante de dash
    public float dashDirection;                 // Dirección del dash
    public float countDashGrounded = 0;         // Contador de dash en suelo
    public float TimeWaitDas = 0.50f;           // Tiempo de espera para dash
    public float TimerDash;

    [Header("Camera stuff")]
    [SerializeField] private GameObject _cameraFollowGO; // Objeto de cámara

    private CameraFollowObject _cameraFollowObject;
    private float _fallSpeedYDampingChangeThreshold; // Umbral de velocidad para cámara

    // --- Inicialización ---
    private void Awake()
    {
        actions = FindAnyObjectByType<PlayerActions>(); // Encuentra acciones del jugador
        isFacingRight = Mathf.Abs(transform.eulerAngles.y) < 1f; // Detecta dirección inicial
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        bc = GetComponent<BoxCollider2D>();

        // Guardar valores originales del collider para agacharse
        originalSizeY = bc.size.y;
        originalOffsetY = bc.offset.y;

        _cameraFollowObject = _cameraFollowGO.GetComponent<CameraFollowObject>();
        _fallSpeedYDampingChangeThreshold = CameraManager.instance._fallSpeedYDampingChangeThreshold;

        isDuck = false;

        if (isFacingRight) FaceRight();
        else FaceLeft();
    }

    void Update()
    {
        // Ajustar cámara al caer
        if (rb.linearVelocity.y < _fallSpeedYDampingChangeThreshold &&
            !CameraManager.instance.IsLerpingYDamping &&
            !CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(true);
        }

        if (rb.linearVelocity.y >= 0f &&
            !CameraManager.instance.IsLerpingYDamping &&
            CameraManager.instance.LerpedFromPlayerFalling)
        {
            CameraManager.instance.LerpYDamping(false);
        }

        // Control de dash
        if (countDashGrounded > 1 && !IsGrounded)
        {
            isDashing = false;
            TimerDash = TimeWaitDas;
        }
        else
        {
            Dash();
        }

        ControlsMove(); // Lee input de movimiento

        if (!isDuck)
            Move(); // Movimiento horizontal y salto

        Duck(); // Control de agacharse
    }

    void FixedUpdate()
    {
        // Revisión de suelo y colisiones
        IsGrounded = CheckIfGrounded();
        CheckWallCollision();
        CheckPlatformCollision();
        CheckIfGroundedTop();
        CheckCornerRays();
        CornerCorrection();

        if (MovePlayer != 0)
            turnCheck(); // Volteo del personaje

        // Movimiento en el aire
        if (!IsGrounded)
        {
            if (!isDashing)
                rb.linearVelocity = new Vector2(MovePlayer * speed, rb.linearVelocity.y);

            if (rb.linearVelocity.y <= 1)
                rb.AddForce(Vector2.down * SpeedFall);
        }

        if (!isDashing)
            rb.gravityScale = GravityScale;
    }

    // --- Movimiento principal ---
    public void Move()
    {
        if (!CanMove) return;

        // Bloquear movimiento si está tocando paredes o plataformas
        if ((isTouchingWallLeft && MovePlayer < 0) ||
            (isTouchingWallRight && MovePlayer > 0) ||
            (isTouchingPlatformLeft && MovePlayer < 0) ||
            (isTouchingPlatformRight && MovePlayer > 0))
        {
            MovePlayer = 0;
        }

        rb.linearVelocity = new Vector2(MovePlayer * speed, rb.linearVelocity.y);

        // Inicio de salto
        if (IsGrounded && Input.GetKeyDown(KeyCode.W) && jumpInput > 0)
        {
            isJumping = true;
            jumpTimeCounter = variableJumpTime;
            Jump(JumpForce);
        }

        // Altura variable de salto
        if (Input.GetKey(KeyCode.W) && isJumping && jumpInput > 0)
        {
            if (jumpTimeCounter > 0)
            {
                Jump(JumpForce + 4);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }

        if (Input.GetKeyUp(KeyCode.W))
            isJumping = false;
    }

    // --- Dash ---
    public void Dash()
    {
        if (TimerDash > 0)
            TimerDash -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && !isDashing && TimerDash <= 0f)
            StartDash();

        if (isDashing)
        {
            DashTimeLeft -= Time.deltaTime;
            rb.linearVelocity = new Vector2(dashDirection * (speed * 2), 0);

            if (DashTimeLeft <= 0)
            {
                isDashing = false;
                rb.gravityScale = GravityScale;

                TimerDash = IsGrounded ? TimeWaitDas : 0f;
            }
        }
    }

    public void StartDash()
    {
        isDashing = true;
        rb.gravityScale = 0;
        DashTimeLeft = DashDuration;

        // Dirección del dash según input o mirando
        dashDirection = MovePlayer != 0
            ? Mathf.Sign(MovePlayer)
            : (isFacingRight ? 1 : -1);

        countDashGrounded++;
    }

    public void Jump(float Jump)
    {
        if (!CanMove) return;

        rb.linearVelocity = new Vector2(rb.linearVelocityX, 0f);
        rb.AddForce(Vector2.up * Jump, ForceMode2D.Impulse);
    }

    // --- Agacharse ---
    public void Duck()
    {
        if (!CanMove) return;

        if (IsGrounded && Input.GetKey(KeyCode.S))
        {
            bc.size = new Vector2(bc.size.x, originalSizeY / 2f);
            bc.offset = new Vector2(bc.offset.x, originalOffsetY - (originalSizeY / 4f));
            isDuck = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            bc.size = new Vector2(bc.size.x, originalSizeY);
            bc.offset = new Vector2(bc.offset.x, originalOffsetY);
            isDuck = false;
        }
    }

    // --- Leer input ---
    public void ControlsMove()
    {
        Vector2 move = ControlPlayer.action.ReadValue<Vector2>();
        MovePlayer = move.x;
        jumpInput = move.y;
    }

    // --- Volteo según dirección ---
    public void turnCheck()
    {
        if (MovePlayer > 0 && lastMoveX <= 0)
            FaceRight();
        else if (MovePlayer < 0 && lastMoveX >= 0)
            FaceLeft();

        lastMoveX = MovePlayer;
    }

    private void FaceRight()
    {
        isFacingRight = true;
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        _cameraFollowObject?.CallTurn(isFacingRight);
    }

    private void FaceLeft()
    {
        isFacingRight = false;
        transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        _cameraFollowObject?.CallTurn(isFacingRight);
    }
}