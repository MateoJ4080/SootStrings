using System.Collections;
using UnityEngine;

public class ShowerMission : MissionInstance
{
    [SerializeField] private DialogueData[] _dialogues;
    [SerializeField] private AudioClip _showerSound;


    void OnEnable()
    {
        GameEvents.OnShowerTaken += OnShowerTaken;
    }

    public override void OnShowerTaken()
    {
        StartCoroutine(Execute());
    }

    private IEnumerator Execute()
    {
        DayManager.Instance.SetPlayerActive(false);

        yield return FadeManager.Instance.FadeIn();
        AudioManager.Instance.PlaySFX(_showerSound);
        yield return new WaitForSeconds(_showerSound.length);
        yield return FadeManager.Instance.FadeOut();

        DayManager.Instance.SetPlayerActive(true);
        StartCoroutine(DialogueManager.Instance.PlaySequence(_dialogues));
        completed = true;
    }
}
