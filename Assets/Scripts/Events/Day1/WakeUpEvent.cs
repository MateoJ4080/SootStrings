using System.Collections;
using UnityEngine;

public class WakeUpEvent : DayEvent
{
    [SerializeField] private PlayerCinematics playerCinematics;
    [SerializeField] private InteractableShower shower;
    [SerializeField] private DebugConfig debugConfig;

    public override IEnumerator Execute()
    {
        if (debugConfig.autoStartDays)
        {
            DayManager.Instance.SetPlayerActive(false);
            yield return _waitForSeconds2;
            yield return playerCinematics.SmoothMoveTo(new(-3.509f, 0.85f, -11.843f), 1f);
            DayManager.Instance.SetPlayerActive(true);
        }

        // yield return UIManager.Instance.ShowDialogue("Ugh… my head is killing me.");
        // yield return UIManager.Instance.ShowDialogue("What the hell did I do last night?");

        yield return _waitForSeconds2;
        shower.IsActive = true;
    }
}
