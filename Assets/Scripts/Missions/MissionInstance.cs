using UnityEngine;

// All event-related methods are declared here so they can be overriden by any mission in their own derived class.
public class MissionInstance : MonoBehaviour
{
    public virtual void OnShowerTaken() { }

    public virtual void OnSlept() { }

    public virtual void OnCellphoneUsed() { }
}
