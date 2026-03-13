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
        LookForInteractable();

        // Debug.DrawRay(_camTransform.position, _camTransform.forward * interactDistance, Color.red);
    }

    void LookForInteractable()
    {
        UIManager.Instance.HideInteractableText();

        Ray ray = new(_camTransform.position, _camTransform.forward);
        currentInteractable = null;

        if (!Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactableMask))
        {
            return;
        }

        if (!hit.collider.TryGetComponent(out IInteractable interactable))
        {
            return;
        }
        float distanceToPlayer = Vector3.Distance(_camTransform.position, hit.point);

        if (distanceToPlayer > interactable.InteractionRange)
        {
            return;
        }

        currentInteractable = interactable;
        if (interactable.IsActive) UIManager.Instance.ShowInteractableText();
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        //Debug.Log($"currentInteractable is{(currentInteractable == null ? "" : "n't")} null");
        if (callbackContext.performed && currentInteractable != null && currentInteractable.IsActive)
        {
            currentInteractable.Interact(gameObject);
        }
    }

    // Show gizmos in the editor
    // void OnDrawGizmosSelected()
    // {
    //     if (_camTransform == null) return;

    //     Gizmos.color = Color.green;
    //     Gizmos.DrawLine(_camTransform.position, _camTransform.position + _camTransform.forward * interactDistance);
    // }
}
