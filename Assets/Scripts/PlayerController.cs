using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(CharacterController), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float walkSpeed = 3.0f;
    [SerializeField] private float sprintSpeed = 6.0f;
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float crouchHeight = 1.0f;
    [SerializeField] private float standHeight = 2.0f;
    [SerializeField] private float headClearanceCheckDistance = 2.5f; // Slightly above standHeight

    [Header("Smoothing Settings")]
    [SerializeField] private float movementSmoothTime = 0.1f; // Time to reach target speed
    [SerializeField] private float crouchSmoothTime = 0.2f; // Time for crouch transition

    private CharacterController controller;
    private Transform headTransform;
    private Transform cameraTransform;
    private Vector2 movementInput;
    private Vector3 velocity;
    private Vector3 currentMoveVelocity;
    private Vector3 moveVelocityDamp;
    private bool isGrounded;
    private bool isSprinting = false;
    private bool isCrouching = false;
    private float targetHeight;
    private float currentHeight;
    private Vector3 targetHeadPosition;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        headTransform = transform.Find("Head");
        if (headTransform == null)
        {
            Debug.LogError("Head transform not found! Please add a child GameObject named 'Head'.");
        }
        cameraTransform = Camera.main.transform;
        targetHeight = standHeight;
        currentHeight = standHeight;
        targetHeadPosition = new Vector3(0, standHeight / 2, 0);
    }

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        // Check if grounded
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to stay grounded
        }

        // Determine movement speed: no sprinting when crouched
        float targetSpeed = (isSprinting && !isCrouching) ? sprintSpeed : walkSpeed;

        // Calculate movement direction based on camera orientation (horizontal plane)
        Vector3 camForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        Vector3 camRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        Vector3 moveDirection = (camForward * movementInput.y + camRight * movementInput.x).normalized;

        // Smooth movement with acceleration
        Vector3 targetMoveVelocity = moveDirection * targetSpeed;
        currentMoveVelocity = Vector3.SmoothDamp(currentMoveVelocity, targetMoveVelocity, ref moveVelocityDamp, movementSmoothTime);

        // Combine horizontal movement and vertical velocity
        Vector3 finalVelocity = currentMoveVelocity + Vector3.up * velocity.y;
        controller.Move(finalVelocity * Time.deltaTime);

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public void OnSprintStarted(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isSprinting = true;
        }
    }

    public void OnSprintCanceled(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            isSprinting = false;
        }
    }

    // public void SmoothCrouchTransition()
    // {
    //     currentHeight = Mathf.Lerp(currentHeight, targetHeight, Time.deltaTime / crouchSmoothTime);
    //     controller.height = currentHeight;
    //     controller.center = new Vector3(0, currentHeight / 2, 0);
    //     headTransform.localPosition = Vector3.Lerp(headTransform.localPosition, targetHeadPosition, Time.deltaTime / crouchSmoothTime);
    // }

    // public void OnCrouch(InputAction.CallbackContext context)
    // {
    //     if (context.performed)
    //     {
    //         if (!isCrouching)
    //         {
    //             // Enter crouch state
    //             isCrouching = true;
    //             targetHeight = crouchHeight;
    //             targetHeadPosition = new Vector3(0, crouchHeight / 2, 0);
    //         }
    //         else
    //         {
    //             // Attempt to uncrouch
    //             Vector3 rayOrigin = transform.position + Vector3.up * crouchHeight / 2;
    //             if (!Physics.Raycast(rayOrigin, Vector3.up, headClearanceCheckDistance))
    //             {
    //                 isCrouching = false;
    //                 targetHeight = standHeight;
    //                 targetHeadPosition = new Vector3(0, standHeight / 2, 0);
    //             }
    //             else
    //             {
    //                 // Optional: Add feedback for obstruction
    //                 Debug.Log("Cannot stand up: Obstacle above player");
    //             }
    //         }
    //     }
    // }
}