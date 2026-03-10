using System.Collections;
using UnityEngine;

public class ShowerMission : MissionInstance
{
    [SerializeField] private InteractableBed interactableBed;

    [SerializeField] private DialogueData[] _dialogues;
    [SerializeField] private AudioClip _showerSound;

    void OnEnable()
    {
        GameEvents.OnShowerTaken += OnShowerTaken;
    }

    void OnDisable()
    {
        GameEvents.OnShowerTaken -= OnShowerTaken;
    }

    public override void OnShowerTaken()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        DayManager.Instance.SetPlayerActive(false);
        yield return FadeManager.Instance.FadeIn();
        StartCoroutine(AudioManager.Instance.PlaySFXCoroutine(_showerSound));
        yield return new WaitForSeconds(_showerSound.length);
        yield return FadeManager.Instance.FadeOut();
        DayManager.Instance.SetPlayerActive(true);

        StartCoroutine(DialogueManager.Instance.PlaySequence(_dialogues));
        // Activated here instead of suscribing it to OnShowerTaken to ensure it happens after the sequence above. 
        interactableBed.Activate();
    }
}
