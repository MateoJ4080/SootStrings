using System;
using UnityEngine;

public class InteractableShower : MonoBehaviour, IInteractable
{
    public bool IsActive => DayManager.Instance.CurrentDay == DayManager.Days.Day1;
    public event Action OnInteracted;

    public void Interact(GameObject gameObject)
    {
        OnInteracted?.Invoke();
    }
}
