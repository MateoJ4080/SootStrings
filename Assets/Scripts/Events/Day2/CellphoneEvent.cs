using System.Collections;
using UnityEngine;

public class CellphoneEvent : DayEvent
{
    [SerializeField] private InteractableCellphone cellphone;
    [SerializeField] private LandlinePhoneEvent landlineEvent;
    private bool _playerInteracted = false;

    void Awake()
    {
        cellphone.OnInteracted += () => _playerInteracted = true;
        cellphone.OnInteracted += () => cellphone.Activate();
    }

    public override IEnumerator Execute()
    {
        yield return new WaitUntil(() => _playerInteracted);

        // yield return UIManager.Instance.ShowDialogue("Is it broken?");
        // yield return UIManager.Instance.ShowDialogue("How...? When...?");

        landlineEvent.OnStartLandlineRing();
        yield break;
    }
}