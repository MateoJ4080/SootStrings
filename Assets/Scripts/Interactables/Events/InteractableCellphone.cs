using System.Collections;
using UnityEngine;

public class InteractableCellphone : Interactable
{
    public override void Interact(GameObject gameObject)
    {
        GameEvents.RaiseOnBrokenCellphoneTaken();
    }
}
