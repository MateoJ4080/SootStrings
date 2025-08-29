using UnityEngine;

public interface IInteractable
{
    bool IsActive { get; }
    void Interact(GameObject gameObject);
}
