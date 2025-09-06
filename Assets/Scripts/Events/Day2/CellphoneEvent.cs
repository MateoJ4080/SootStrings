using System.Collections;
using UnityEngine;

public class CellphoneEvent : DayEvent
{
    [SerializeField] private InteractableCellphone cellphone;
    private bool _playerInteracted = false;

    void Awake()
    {
        cellphone.OnInteracted += () => _playerInteracted = true;
    }

    public override IEnumerator Execute()
    {
        yield return new WaitUntil(() => _playerInteracted);
        cellphone.IsActive = false;

        yield return UIManager.Instance.ShowDialogue("Is it broken?");
        yield return UIManager.Instance.ShowDialogue("How...? When...?");
        yield break;
    }
}