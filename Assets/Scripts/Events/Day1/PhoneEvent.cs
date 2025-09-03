using System.Collections;
using UnityEngine;

public class PhoneEvent : DayEvent
{
    [SerializeField] private InteractablePhone phone;

    public override IEnumerator Execute()
    {
        phone.Ring();
        yield return new WaitUntil(() => !phone.IsActive);
        yield return UIManager.Instance.ShowDialogue("Hello? Who is this?");
        yield break;
    }
}
