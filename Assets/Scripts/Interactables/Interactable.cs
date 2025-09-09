using UnityEngine;

public interface IInteractable
{
    float InteractionRange { get; }
    bool IsActive { get; set; }
    void Interact(GameObject gameObject);
}
