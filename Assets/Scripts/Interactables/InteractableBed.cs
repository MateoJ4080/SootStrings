using System;
using UnityEngine;

public class InteractableBed : MonoBehaviour, IInteractable
{
    [SerializeField] private ShowerEvent showerEvent;

    public bool IsActive => showerEvent.PlayerHasShowered;
    public event Action OnInteracted;

    public void Interact(GameObject gameObject)
    {
        OnInteracted?.Invoke();
    }
}
