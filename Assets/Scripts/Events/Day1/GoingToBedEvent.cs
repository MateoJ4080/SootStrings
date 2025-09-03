using System.Collections;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;

public class GoingToBedEvent : DayEvent
{
    [SerializeField] private InteractableBed bed;
    private bool _playerInteracted = false;

    void Awake()
    {
        bed.OnInteracted += () => _playerInteracted = true;
    }

    public override IEnumerator Execute()
    {
        yield return new WaitUntil(() => _playerInteracted);
        yield return FadeManager.Instance.FadeIn();
        yield break;
    }
}
