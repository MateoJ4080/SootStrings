using System.Collections;
using TMPro.EditorUtilities;
using UnityEngine;

public class LandlineMission : MissionInstance
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private InteractableLandline _interactableLandline;
    [SerializeField] private InteractableCellphone _interactableCellphone;
    [SerializeField] private Camera _cam;
    [SerializeField] private GameObject _faintTrigger;
    [SerializeField] private DialogueData[] _dialogues;
    [SerializeField] private AudioClip _faintSfx;
    [SerializeField] private AudioSource landlineSource;
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
        _interactableCellphone.Deactivate();
        yield return StartCoroutine(DialogueManager.Instance.PlaySequence(_dialogues));

        RingLandline();

        while (!fainted)
        {
            float distance = Vector3.Distance(
                _playerController.transform.position,
                _faintTrigger.transform.position);
            float t = Mathf.InverseLerp(0f, startDistance, distance); // 1 if player is at startDistance and 0 if at faintTrigger pos

            // Slowdown
            float speed = Mathf.Lerp(0.1f, 1f, t);
            _playerController.SetSpeedMultiplier(speed);

            // Fade view
            if (t < 0.7f)
            {
                float fade = Mathf.InverseLerp(0.7f, 0.05f, t);
                FadeManager.Instance.SetAlpha(fade);
            }

            // Increase fov
            _cam.fieldOfView = Mathf.Lerp(155, 60, t);

            // Lower landline volume
            landlineSource.volume = t * 0.1f;

            Debug.Log(t);

            if (t == 0) fainted = true;

            yield return null;
        }
        DayManager.Instance.SetPlayerActive(false);
        FadeManager.Instance.SetAlpha(1f);
        AudioManager.Instance.PlaySFX(_faintSfx);
        _playerController.SetSpeedMultiplier(1f);
    }

    private void RingLandline()
    {
        _interactableLandline.Ring();
        _faintTrigger.SetActive(true);
    }

    public void OnFaintTrigger() => fainted = true;
}