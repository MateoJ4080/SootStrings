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
        set => isActive = value;
    }

    public event Action OnInteracted;

    public void Interact(GameObject gameObject)
    {
        OnInteracted?.Invoke();
    }
}
