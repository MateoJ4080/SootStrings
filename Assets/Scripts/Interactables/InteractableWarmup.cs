using UnityEngine;

public class InteractableWarmup : MonoBehaviour
{
    void Awake()
    {
        // Create a temporary object that implements IInteractable
        var dummy = new GameObject("DummyInteractable");
        dummy.AddComponent<DummyInteractable>();

        // Force the generic call that causes the first-time lag
        if (dummy.TryGetComponent<IInteractable>(out _))
        {
            // Do nothing, just forces the JIT compilation
        }

        // Destroy it immediately after
        Destroy(dummy);
    }
}

// Minimal class to "wake up" the JIT
public class DummyInteractable : MonoBehaviour, IInteractable
{
    public bool IsActive { get; set; } = false;
    public float InteractionRange { get; set; } = 1f;
    public void Interact(GameObject interactor) { }
}