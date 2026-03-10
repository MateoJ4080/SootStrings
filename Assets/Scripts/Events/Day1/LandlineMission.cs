using System.Collections;
using UnityEngine;

public class LandlineMission : MissionInstance
{
    [SerializeField] private DialogueData[] _dialogues;
    [SerializeField] private InteractableLandline landlineInteractable;
    [SerializeField] private GameObject faintTrigger;
    [SerializeField] private AudioClip fiantSound;

    private bool fainted = false;

    void OnEnable()
    {
        GameEvents.OnBrokenCellphoneTaken += OnBrokenCellphoneTaken;
    }

    void OnDisable()
    {
        GameEvents.OnBrokenCellphoneTaken -= OnBrokenCellphoneTaken;
    }

    void OnBrokenCellphoneTaken()
    {
        StartCoroutine(Execute());
    }

    public IEnumerator Execute()
    {
        yield return StartCoroutine(DialogueManager.Instance.PlaySequence(_dialogues));

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

        landlineInteractable.Ring();
        faintTrigger.SetActive(true);
        yield return FadeManager.Instance.FadeTo(0.5f, 3f);
    }

    public void OnFaintTrigger() => fainted = true;
}