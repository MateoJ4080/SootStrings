using System;
using UnityEngine;

public class InteractableBed : MonoBehaviour, IInteractable
{
    private bool isActive;
    public bool IsActive
    {
        get => isActive;
        set => isActive = value;
    }

    public event Action OnInteracted;

    public void Interact(GameObject gameObject)
    {
        OnInteracted?.Invoke();
    }
}
