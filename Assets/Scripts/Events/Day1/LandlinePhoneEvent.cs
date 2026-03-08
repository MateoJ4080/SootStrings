using System.Collections;
using UnityEngine;

public class LandlinePhoneEvent : DayEvent
{
    [SerializeField] private InteractableLandlinePhone landlinePhone;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject faintTrigger;
    [SerializeField] private AudioClip fiantSound;

    private bool effectsStarted = false;
    private bool fainted = false;

    public override IEnumerator Execute()
    {
        yield return new WaitUntil(() => effectsStarted);

        StartCoroutine(RingLandline());

        yield return new WaitUntil(() => fainted);
        DayManager.Instance.SetPlayerActive(false);
        yield return FadeManager.Instance.FadeIn();
        yield return AudioManager.Instance.PlaySFXCoroutine(fiantSound);
    }

    private IEnumerator RingLandline()
    {
        // Vector3 direction = faintTriggerZone.transform.position - player.transform.position;
        // float distance = direction.magnitude;

        yield return landlinePhone.Ring();
        faintTrigger.SetActive(true);
        yield return FadeManager.Instance.FadeTo(0.5f, 3f);
    }

    public void OnStartLandlineRing() => effectsStarted = true;
    public void OnFaintTrigger() => fainted = true;
}