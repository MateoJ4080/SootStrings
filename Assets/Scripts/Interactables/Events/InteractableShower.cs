using System;
using UnityEngine;

public class InteractableShower : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionRange = 2f;
    public float InteractionRange => interactionRange;

    private bool isActive;
    public bool IsActive
    {
        get => isActive;
    }

    public void Interact(GameObject gameObject)
    {
        GameEvents.RaiseOnShowerTaken();
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
