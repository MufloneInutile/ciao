using UnityEngine;

/// <summary>
/// Movimento base di un personaggio in terza/prima persona.
/// Da attaccare a un GameObject che ha un componente CharacterController.
/// Gestisce: spostamento WASD, salto e gravità.
/// </summary>
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimento")]
    [Tooltip("Velocità di camminata in unità/secondo.")]
    public float moveSpeed = 6f;

    [Tooltip("Velocità di corsa (tenendo premuto Shift).")]
    public float runSpeed = 10f;

    [Header("Salto e gravità")]
    [Tooltip("Altezza massima del salto in unità.")]
    public float jumpHeight = 1.5f;

    [Tooltip("Forza di gravità (negativa).")]
    public float gravity = -19.62f;

    [Header("Controllo a terra")]
    [Tooltip("Punto da cui verificare il contatto col terreno (un GameObject vuoto ai piedi).")]
    public Transform groundCheck;

    [Tooltip("Raggio della sfera usata per il controllo del terreno.")]
    public float groundDistance = 0.3f;

    [Tooltip("Layer considerati come 'terreno'.")]
    public LayerMask groundMask;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleGroundCheck();
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    /// <summary>
    /// Verifica se il personaggio è a terra.
    /// </summary>
    private void HandleGroundCheck()
    {
        if (groundCheck != null)
        {
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        }
        else
        {
            // Fallback: usa il CharacterController se non è impostato un groundCheck.
            isGrounded = controller.isGrounded;
        }

        // Mantiene il personaggio incollato al suolo quando è a terra.
        if (isGrounded && velocity.y < 0f)
        {
            velocity.y = -2f;
        }
    }

    /// <summary>
    /// Sposta il personaggio in base agli input orizzontali/verticali.
    /// </summary>
    private void HandleMovement()
    {
        float x = Input.GetAxis("Horizontal"); // A / D
        float z = Input.GetAxis("Vertical");   // W / S

        // Direzione relativa all'orientamento del personaggio.
        Vector3 move = transform.right * x + transform.forward * z;

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Esegue il salto se il personaggio è a terra.
    /// </summary>
    private void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // v = sqrt(2 * h * -g) -> velocità iniziale per raggiungere jumpHeight.
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    /// <summary>
    /// Applica la gravità in modo continuo.
    /// </summary>
    private void ApplyGravity()
    {
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    // Disegna in editor la sfera di controllo del terreno.
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundDistance);
        }
    }
}
