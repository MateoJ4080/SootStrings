using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float playerGravity = -9.81f;
    [SerializeField] private float movementSmoothTime = 0.1f; // Time to reach target speed

    private CharacterController controller;
    private Transform headTransform;
    private Transform cameraTransform;
    private Vector2 movementInput;
    private Vector3 velocity;
    private Vector3 currentMoveVelocity;
    private Vector3 moveVelocityDamp;
    private bool isGrounded;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        headTransform = transform.Find("Head");
        if (headTransform == null)
        {
            Debug.LogError("Head transform not found! Please add a child GameObject named 'Head'.");
        }
        cameraTransform = Camera.main.transform;
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        // Check if grounded and apply downward force in player if not
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // Calculate movement direction based on camera orientation (horizontal plane)
        Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 moveDirection = (camForward * movementInput.y + camRight * movementInput.x).normalized;

        // Smooth movement with acceleration
        Vector3 targetMoveVelocity = moveDirection * walkSpeed;
        currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, targetMoveVelocity, ref moveVelocityDamp, movementSmoothTime);

        // Combine horizontal movement and vertical velocity
        Vector3 finalVelocity = currentMoveVelocity + Vector3.up * velocity.y;
        controller.Move(finalVelocity * Time.deltaTime);

        // Apply gravity
        velocity.y += playerGravity * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * playerGravity);
        }
    }
}