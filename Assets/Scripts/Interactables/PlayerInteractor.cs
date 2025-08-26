using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Transform _camTransform;
    [SerializeField] private float interactDistance;

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();
    }

    void CheckForInteractable()
    {
        UIManager.Instance.ShowInteractableText(false);

        Ray ray = new(_camTransform.position, _camTransform.forward * interactDistance);
        currentInteractable = null;

        if (!Physics.Raycast(ray, out RaycastHit hit))
        {
            return;
        }

        if (!hit.collider.TryGetComponent(out IInteractable interactable))
        {
            return;
        }

        Debug.Log("There's an interactable");

        currentInteractable = interactable;
        UIManager.Instance.ShowInteractableText(interactable.CanInteract);
    }

    public void OnInteract(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Trying OnInteract");
        //Debug.Log($"currentInteractable is{(currentInteractable == null ? "" : "n't")} null");
        if (callbackContext.performed && currentInteractable != null)
        {
            Debug.Log("OnInteract");
            currentInteractable.Interact(gameObject);
        }
    }

    // Editor
    void OnDrawGizmosSelected()
    {
        if (_camTransform == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(_camTransform.position, _camTransform.position + _camTransform.forward * interactDistance);
    }
}
