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
        yield return new WaitUntil(() => _playerInteracted);
        yield return FadeManager.Instance.FadeIn();
        Debug.Log("Showering...");
        yield return AudioManager.Instance.PlaySFX(showerSound, 0.2f);
        yield return new WaitForSeconds(showerSound.length);
        yield return null;
    }
}
