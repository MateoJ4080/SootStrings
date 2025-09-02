using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Transform _camTransform;

    [SerializeField] private float interactDistance;
    [SerializeField] private LayerMask interactableMask;

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        Debug.DrawRay(_camTransform.position, _camTransform.forward * interactDistance, Color.red);

    }

    void CheckForInteractable()
    {
        UIManager.Instance.ShowInteractableText(false);

        Ray ray = new(_camTransform.position, _camTransform.forward);
        currentInteractable = null;

        if (!Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableMask))
        {
            return;
        }

        if (!hit.collider.TryGetComponent(out IInteractable interactable))
        {
            Debug.Log($"Hit {hit.collider.name}. Distance: {hit.distance}");
            return;
        }

        Debug.Log($"Hit {hit.collider.name}. Distance: {hit.distance}");


        currentInteractable = interactable;
        UIManager.Instance.ShowInteractableText(interactable.IsActive);
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Trying OnInteract");
        //Debug.Log($"currentInteractable is{(currentInteractable == null ? "" : "n't")} null");
        if (callbackContext.performed && currentInteractable != null && currentInteractable.IsActive)
        {
            Debug.Log("OnInteract");
            currentInteractable.Interact(gameObject);
        }
    }

    // Show gizmos in the editor
    void OnDrawGizmosSelected()
    {
        if (_camTransform == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(_camTransform.position, _camTransform.position + _camTransform.forward * interactDistance);
    }
}
