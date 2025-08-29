using System.Collections;
using UnityEngine;

public abstract class DayEvent : MonoBehaviour
{
    public WaitForSeconds _waitForSeconds2 = new(2f);
    public float fixedEventIntervalTime = 3f;
    public abstract IEnumerator Execute();
}
