using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeInDuration;
    [SerializeField] private float _fadeOutDuration;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        _canvasGroup = FindFirstObjectByType<CanvasGroup>();
    }

    private IEnumerator Fade(float start, float end, float duration)
    {
        float timer = 0f;

        if (_canvasGroup == null) yield break;

        _canvasGroup.alpha = start;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float easedT = t * t;
            _canvasGroup.alpha = Mathf.Lerp(start, end, easedT);
            yield return null;
        }

        _canvasGroup.alpha = end;
    }

    public IEnumerator FadeIn() => Fade(_canvasGroup.alpha, 1f, _fadeInDuration);
    public IEnumerator FadeOut() => Fade(_canvasGroup.alpha, 0f, _fadeOutDuration);

    public IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = _canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            time += Time.unscaledDeltaTime;
            _canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            yield return null;
        }

        _canvasGroup.alpha = targetAlpha;
    }

    public void SetAlpha(float value)
    {
        _canvasGroup.alpha = value;
    }

    // ====== TESTING ======
    [ContextMenu("Test FadeIn")]
    private void TestFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    [ContextMenu("Test FadeOut")]
    private void TestFadeOut()
    {
        StartCoroutine(FadeOut());
    }
}
