using System.Collections;
using UnityEngine;

public class GoingToBedEvent : DayEvent
{
    private bool _playerInteracted = false;

    [SerializeField] private InteractableBed bed;
    [SerializeField] private InteractableCellphone cellphone;
    [SerializeField] private GameObject landlinePhoneTrigger;

    void Awake()
    {
        bed.OnInteracted += () => _playerInteracted = true;
    }

    public override IEnumerator Execute()
    {
        yield return new WaitUntil(() => _playerInteracted);
        bed.IsActive = false;

        yield return FadeManager.Instance.FadeIn();
        yield return FadeManager.Instance.FadeOut();
        cellphone.IsActive = true;
        landlinePhoneTrigger.SetActive(true);
        yield break;
    }
}
