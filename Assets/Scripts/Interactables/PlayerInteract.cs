using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float _interactionDistance = 3f;
    [SerializeField] private LayerMask _interactableLayer = 6;
    [SerializeField] private Transform _cameraTransform;
    private Interactable _currentInteractable;

    private void FixedUpdate()
    {
        DetectInteractable();
    }


    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed && _currentInteractable != null)
        {
            _currentInteractable.Interact(gameObject);
        }
    }

    private void DetectInteractable()
    {
        Ray ray = new Ray(_cameraTransform.position, _cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, _interactionDistance, _interactableLayer))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                _currentInteractable = interactable;
            }
        }

    }
}
