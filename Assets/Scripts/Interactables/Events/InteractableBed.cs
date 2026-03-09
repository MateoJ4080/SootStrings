using System;
using UnityEngine;

public class InteractableBed : MonoBehaviour, IInteractable
{
    [SerializeField] private readonly float _interactionRange = 2f;
    public float InteractionRange => _interactionRange;

    private bool isActive;
    public bool IsActive { get => isActive; }

    public void Interact(GameObject gameObject)
    {
        GameEvents.RaiseOnSlept();
    }

    public void Activate()
    {
        isActive = true;
    }

    public void Deactivate()
    {
        isActive = false;
    }
}
