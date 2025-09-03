using System.Collections;
using UnityEngine;

public class PhoneEvent : DayEvent
{
    [SerializeField] private InteractablePhone phone;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject faintTriggerZone;
    [SerializeField] private AudioClip fiantSound;

    private bool effectsStarted = false;
    private bool fainted = false;

    public override IEnumerator Execute()
    {
        yield return new WaitUntil(() => effectsStarted);

        StartCoroutine(DoEffects());

        yield return new WaitUntil(() => fainted);
        yield return FadeManager.Instance.FadeIn();
        yield return AudioManager.Instance.PlaySFX(fiantSound);
    }

    private IEnumerator DoEffects()
    {
        // Vector3 direction = faintTriggerZone.transform.position - player.transform.position;
        // float distance = direction.magnitude;

        phone.Ring();
        faintTriggerZone.SetActive(true);
        yield return FadeManager.Instance.FadeTo(0.5f, 3f);
    }

    public void OnStartEffectsTrigger() => effectsStarted = true;
    public void OnFaintTrigger() => fainted = true;
}