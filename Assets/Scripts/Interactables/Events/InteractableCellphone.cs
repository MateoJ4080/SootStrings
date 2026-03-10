using UnityEngine;

public class InteractableCellphone : MonoBehaviour, IInteractable
{
    [SerializeField] private readonly float _interactionRange = 2f;
    public float InteractionRange => _interactionRange;

    private bool isActive;
    public bool IsActive { get => isActive; }

    public void Interact(GameObject gameObject)
    {
        GameEvents.RaiseOnBrokenCellphoneTaken();
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
