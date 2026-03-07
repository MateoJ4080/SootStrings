using UnityEngine;

public interface IInteractable
{
    float InteractionRange { get; }
    bool IsActive { get; }
    void Interact(GameObject gameObject);
    void Activate();
    void Deactivate();
}
