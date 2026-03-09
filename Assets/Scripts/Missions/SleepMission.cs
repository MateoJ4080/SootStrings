using System.Collections;
using UnityEngine;

public class SleepMission : MissionInstance
{
    [SerializeField] private InteractableBed bed;
    [SerializeField] private InteractableCellphone cellphone;

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

    public IEnumerator Execute()
    {
        Debug.Log("SleepMission Execute");
        bed.Deactivate();

        yield return FadeManager.Instance.FadeIn();
        yield return FadeManager.Instance.FadeOut();

        yield break;
    }
}
