using System.Collections;
using UnityEngine;

public class WakeUpEvent : DayEvent
{
    public override IEnumerator Execute()
    {
        yield return FadeManager.Instance.FadeOut();

        yield return new WaitForSeconds(2f);

        // UIManager.ShowDialogue("Hello");
    }
}
