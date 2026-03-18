using System.Collections;
using UnityEngine;

public class IntroWakeUpMission : MissionInstance
{
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerCinematics playerCinematics;
    [SerializeField] private InteractableShower shower;
    [SerializeField] private DebugConfig debugConfig;
    [SerializeField] private DialogueContainer _dialogues;

    public WaitForSeconds _waitForSeconds2 = new(2f);

    void Start()
    {
        StartCoroutine(Execute());
    }

    public IEnumerator Execute()
    {
        if (!debugConfig.debugModeEnabled)
        {
            DayManager.Instance.SetPlayerActive(false);
            yield return _waitForSeconds2;
            yield return playerCinematics.SmoothMoveTo(new(-3.509f, 0.85f, -11.843f), 1f);
            DayManager.Instance.SetPlayerActive(true);
        }
        else
        {
            player.transform.position = new(-2.4f, 0.8f, -5);
        }

        // yield return UIManager.Instance.ShowDialogue("Ugh… my head is killing me.");
        // yield return UIManager.Instance.ShowDialogue("What the hell did I do last night?");

        yield return _waitForSeconds2;
        shower.Activate();
    }
}
