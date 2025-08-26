using UnityEngine;

public class InteractableShower : IInteractable
{
    public bool CanInteract => DayManager.Instance.CurrentDay == DayManager.Days.Day1;

    public void Interact(GameObject gameObject)
    {

    }
}
