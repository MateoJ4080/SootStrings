using System.Collections;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    private CanvasGroup canvasGroup;
    public float fadeInDuration;
    public float fadeOutDuration;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        canvasGroup = FindFirstObjectByType<CanvasGroup>();
    }

    private IEnumerator Fade(float start, float end, float duration)
    {
        float timer = 0f;

        if (canvasGroup == null) yield break;

        canvasGroup.alpha = start;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float easedT = t * t;
            canvasGroup.alpha = Mathf.Lerp(start, end, easedT);
            yield return null;
        }

        canvasGroup.alpha = end;
    }

    public IEnumerator FadeIn() => Fade(canvasGroup.alpha, 1f, fadeInDuration);
    public IEnumerator FadeOut() => Fade(canvasGroup.alpha, 0f, fadeOutDuration);

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
