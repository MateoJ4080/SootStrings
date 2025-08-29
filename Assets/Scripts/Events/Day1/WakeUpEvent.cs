using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WakeUpEvent : DayEvent
{
    public override IEnumerator Execute()
    {
        Debug.Log("Executing WakeUpEvent");
        yield return _waitForSeconds2;
        yield return UIManager.Instance.ShowDialogue("Ugh... where am I?");
        yield return fixedEventIntervalTime;
    }
}
