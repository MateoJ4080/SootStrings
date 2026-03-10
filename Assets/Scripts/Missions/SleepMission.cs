using System.Collections;
using UnityEngine;

public class SleepMission : MissionInstance
{
    [SerializeField] private InteractableBed interactableBed;
    [SerializeField] private InteractableCellphone interactableCellphone;

    void OnEnable()
    {
        GameEvents.OnSlept += OnSlept;
    }

    void OnDisable()
    {
        GameEvents.OnSlept -= OnSlept;
    }

    public override void OnSlept()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        Debug.Log("SleepMission Execute");

        interactableBed.Deactivate();
        yield return FadeManager.Instance.FadeIn();
        yield return FadeManager.Instance.FadeOut();
        interactableCellphone.Activate();

        yield break;
    }
}
