using System.Collections;
using UnityEngine;

public class ShowerEvent : DayEvent
{
    [SerializeField] private AudioClip showerSound;
    private bool _playerInteracted = false;

    [SerializeField] private InteractableBed bed;

    public void Initialize(InteractableShower shower)
    {
        shower.OnInteracted += () => _playerInteracted = true;
        shower.OnInteracted += () => UIManager.Instance.HideObjectiveText();
        shower.OnInteracted += () => bed.IsActive = true;
    }

    public override IEnumerator Execute()
    {
        UIManager.Instance.ShowDialogue("Ugh this headache... I don't feel like doing anything...");
        UIManager.Instance.ShowObjectiveText("Take a shower and go to bed to relieve stress");

        yield return new WaitUntil(() => _playerInteracted);

        // Fade in and shower effect
        DayManager.Instance.SetPlayerActive(false);
        yield return FadeManager.Instance.FadeIn();
        yield return AudioManager.Instance.PlaySFX(showerSound, 0.2f);
        yield return new WaitForSeconds(showerSound.length);
        yield return FadeManager.Instance.FadeOut();
        DayManager.Instance.SetPlayerActive(true);

        yield return null;
    }
}
