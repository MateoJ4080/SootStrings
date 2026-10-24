using UnityEngine;

public class SyncRotation : MonoBehaviour
{
    [SerializeField] private Transform _playerTf;
    [SerializeField] private CameraController _camController;
    [SerializeField] private Animator _animator;

    private bool _pendingSync;

    // Called by the animation event. Sync runs in LateUpdate, after the Animator.
    public void SyncPlayerToTransform()
    {
        _pendingSync = true;
    }

    private void LateUpdate()
    {
        if (!_pendingSync) return;
        _pendingSync = false;

        float yaw = transform.eulerAngles.y;
        float pitch = transform.localEulerAngles.x;
        if (pitch > 180f) pitch -= 360f;

        Vector3 pos = transform.position;
        _playerTf.SetPositionAndRotation(new Vector3(pos.x, _playerTf.position.y, pos.z), Quaternion.Euler(0f, yaw, 0f));

        // Player already moved to the pivot, so zero the pivot to avoid applying the offset twice
        transform.SetLocalPositionAndRotation(new Vector3(0f, transform.localPosition.y, 0f), Quaternion.Euler(pitch, 0f, 0f));

        // Stops the Animator from overwriting this pose next frame
        _animator.enabled = false;

        _camController.xRotation = pitch;
    }
}