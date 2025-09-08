using UnityEngine;

public interface IInteractable
{
    bool IsActive { get; set; }
    void Interact(GameObject gameObject);
}
