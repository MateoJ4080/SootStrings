using System.Collections;
using UnityEngine;

public class ShowerEvent : DayEvent
{
    [SerializeField] private AudioClip showerSound;

    private bool playerHasShowered = false;
    public bool PlayerHasShowered => playerHasShowered;
    private bool _playerInteracted = false;

    public void Initialize(InteractableShower shower)
    {
        shower.OnInteracted += () => _playerInteracted = true;
        shower.OnInteracted += () => UIManager.Instance.HideObjectiveText();
    }

    public override IEnumerator Execute()
    {
        Debug.Log("ShowerEvent");
        UIManager.Instance.ShowDialogue("Ugh this headache... I don't feel like doing anything...");
        UIManager.Instance.ShowObjectiveText("Take a shower and go to bed to relieve stress");
        yield return new WaitUntil(() => _playerInteracted);
        DayManager.Instance.SetPlayerActive(false);
        yield return FadeManager.Instance.FadeIn();
        yield return AudioManager.Instance.PlaySFX(showerSound, 0.2f);
        yield return new WaitForSeconds(showerSound.length);
        playerHasShowered = true;
        yield return FadeManager.Instance.FadeOut();
        DayManager.Instance.SetPlayerActive(true);

        yield return null;
    }
}
