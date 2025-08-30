using System.Collections;
using UnityEngine;

public class ShowerEvent : DayEvent
{
    [SerializeField] private AudioClip showerSound;
    private bool _playerInteracted = false;

    public void Initialize(InteractableShower shower)
    {
        shower.OnInteracted += () => _playerInteracted = true;
    }

    public override IEnumerator Execute()
    {
        Debug.Log("ShowerEvent");
        // UI.Instance.ShowObjective("Take a shower to relieve stress");
        yield return new WaitUntil(() => _playerInteracted);
        DayManager.Instance.SetPlayerActive(false);
        yield return FadeManager.Instance.FadeIn();
        yield return AudioManager.Instance.PlaySFX(showerSound, 0.2f);
        yield return new WaitForSeconds(showerSound.length);
        DayManager.Instance.SetPlayerActive(true);

        yield return null;
    }
}
