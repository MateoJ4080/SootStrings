using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class GoingToBedEvent : DayEvent
{
    [SerializeField] private InteractableBed bed;
    private bool _playerInteracted = false;

    [SerializeField] private GameObject phoneTriggerZone;

    void Awake()
    {
        bed.OnInteracted += () => _playerInteracted = true;
    }

    public override IEnumerator Execute()
    {
        yield return new WaitUntil(() => _playerInteracted);
        yield return FadeManager.Instance.FadeIn();
        yield return FadeManager.Instance.FadeOut();
        phoneTriggerZone.SetActive(true);
        yield break;
    }
}
