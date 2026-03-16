using System.Collections;
using UnityEngine;

public class LandlineMission : MissionInstance
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private InteractableLandline _interactableLandline;
    [SerializeField] private InteractableCellphone _interactableCellphone;
    [SerializeField] private GameObject _faintTrigger;
    [SerializeField] private DialogueData[] _dialogues;
    [SerializeField] private AudioClip _faintSfx;
    float startDistance;

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
        startDistance = Vector3.Distance(
            _playerController.transform.position,
            _faintTrigger.transform.position);

        StartCoroutine(Execute());
    }

    public IEnumerator Execute()
    {
        yield return StartCoroutine(DialogueManager.Instance.PlaySequence(_dialogues));

        StartCoroutine(RingLandline());

        while (!fainted)
        {
            float distance = Vector3.Distance(
                _playerController.transform.position,
                _faintTrigger.transform.position);

            float t = Mathf.InverseLerp(0f, startDistance, distance);
            float speed = Mathf.Lerp(0.1f, 1f, t);

            _playerController.SetSpeedMultiplier(speed);

            yield return null;
        }

        DayManager.Instance.SetPlayerActive(false);
        yield return FadeManager.Instance.FadeIn();
        yield return AudioManager.Instance.PlaySFXCoroutine(_faintSfx);

        _playerController.SetSpeedMultiplier(1f);
    }

    private IEnumerator RingLandline()
    {
        _interactableLandline.Ring();
        _faintTrigger.SetActive(true);
        yield return FadeManager.Instance.FadeTo(0.5f, 3f);
    }

    public void OnFaintTrigger() => fainted = true;
}