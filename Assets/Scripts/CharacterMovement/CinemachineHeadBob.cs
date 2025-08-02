using UnityEngine;
using Unity.Cinemachine;

public class HeadBob : MonoBehaviour
{
    [SerializeField] private CharacterController characterController;
    [SerializeField] private CinemachineFollow cinemachineFollow;
    [SerializeField] private float bobFrequency = 3f; // Increased frequency for stronger feel
    [SerializeField] private float yBobAmplitude = 0.3f; // Stronger vertical bob amplitude
    [SerializeField] private float xBobAmplitude = 0.2f; // Horizontal bob amplitude
    [SerializeField] private float bobSmoothing = 0.2f; // Smoothing for bob transition

    private Vector3 originalFollowOffset;
    private float bobTimer = 0f;
    private Vector3 targetOffset;
    private Vector3 currentOffset;

    void Start()
    {
        if (cinemachineFollow == null)
        {
            cinemachineFollow = GetComponent<CinemachineFollow>();
        }
        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }

        // Store the original follow offset
        originalFollowOffset = cinemachineFollow.FollowOffset;
        currentOffset = originalFollowOffset;
        targetOffset = originalFollowOffset;
    }

    void Update()
    {
        // Get horizontal velocity (ignore vertical/Y component)
        Vector3 horizontalVelocity = new Vector3(characterController.velocity.x, 0f, characterController.velocity.z);

        if (horizontalVelocity.magnitude > 0.1f)
        {
            // Character is moving, apply headbob
            bobTimer += Time.deltaTime * bobFrequency * horizontalVelocity.magnitude;
            float bobOffsetY = Mathf.Sin(bobTimer) * yBobAmplitude; // Vertical bob
            float bobOffsetX = Mathf.Cos(bobTimer * 0.5f) * xBobAmplitude; // Horizontal bob, half frequency for sway
            targetOffset = originalFollowOffset + new Vector3(bobOffsetX, bobOffsetY, 0f);
        }
        else
        {
            // Character is still, reset to original offset
            targetOffset = originalFollowOffset;
            bobTimer = 0f; // Reset timer to avoid abrupt jumps
        }

        // Smoothly interpolate to the target offset
        currentOffset = Vector3.Lerp(currentOffset, targetOffset, bobSmoothing * Time.deltaTime);
        cinemachineFollow.FollowOffset = currentOffset;
    }
}