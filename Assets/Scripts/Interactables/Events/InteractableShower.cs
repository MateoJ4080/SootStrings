using System;
using System.Collections;
using UnityEngine;

public class InteractableShower : Interactable
{
    [SerializeField] private InteractableBed interactableBed;

    [SerializeField] private DialogueData[] _dialogues;
    [SerializeField] private AudioClip _showerSound;

    public override void Interact(GameObject gameObject)
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

        interactableBed.Activate();
    }
}