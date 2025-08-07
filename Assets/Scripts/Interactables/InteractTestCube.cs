using UnityEngine;

public class InteractTestCube : Interactable 
{
    public override void Interact(GameObject gameObject)
    {
        Debug.Log("Cube was touched");
    }
}
