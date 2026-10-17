using UnityEngine;

public abstract class Interactable : MonoBehaviour, IInteractable
{
    [SerializeField] private float _interactionRange = 2f;

    public float InteractionRange => _interactionRange;
    public bool IsActive { get; private set; }

    public abstract void Interact(GameObject gameObject);

    public void Activate() => IsActive = true;
    public void Deactivate() => IsActive = false;
}