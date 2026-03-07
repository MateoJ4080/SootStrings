using System;
using UnityEngine;

public class InteractableCellphone : MonoBehaviour, IInteractable
{
    [SerializeField] private float interactionRange = 2f;
    public float InteractionRange => interactionRange;

    private bool isActive;
    public bool IsActive { get => isActive; }

    public event Action OnInteracted;

    public void Interact(GameObject gameObject)
    {
        OnInteracted?.Invoke();
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
