using System.Collections;
using UnityEngine;

public class IntroWakeUpMission : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private CameraController _camController;
    [SerializeField] private Transform _head;

    [SerializeField] private Animator _introAnimator;
    [SerializeField] private AnimationClip _wakeUpClip;
    [SerializeField] private DialogueContainer _dialogues;
    [SerializeField] private InteractableShower shower;

    public WaitForSeconds _waitForSeconds2 = new(2f);

    public IEnumerator Start()
    {
        DayManager.Instance.SetPlayerActive(false);
        _camController.enabled = false;

        _introAnimator.Play("WakeUp");
        yield return new WaitForSeconds(_wakeUpClip.length * 0.5f); // ----- '2' FOR DEBUG ONLY -----

        // yield return UIManager.Instance.ShowDialogue("Ughhh… my head is killing me.");
        // yield return UIManager.Instance.ShowDialogue("What the hell did I do last night?");

        _camController.enabled = true;
        DayManager.Instance.SetPlayerActive(true);

        yield return _waitForSeconds2;
        shower.Activate();
    }
}