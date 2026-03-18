using System.Collections;
using UnityEngine;

public class SleepMission : MissionInstance
{
    [SerializeField] private InteractableBed _interactableBed;
    [SerializeField] private InteractableCellphone _interactableCellphone;

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
        _interactableBed.Deactivate();
        yield return FadeManager.Instance.FadeIn();
        yield return FadeManager.Instance.FadeOut();
        _interactableCellphone.Activate();

        yield break;
    }
}
