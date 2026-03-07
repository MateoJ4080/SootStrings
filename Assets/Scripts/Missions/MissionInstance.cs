using UnityEngine;

public class MissionInstance : MonoBehaviour
{
    protected bool completed;

    // All event-related methods are declared here so they can be overriden by any mission in their own derived class.
    public virtual void OnShowerTaken() { }
    public virtual void OnSlept() { }
}
