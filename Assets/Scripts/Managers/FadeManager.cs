using System.Threading.Tasks;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    private CanvasGroup canvasGroup;
    public float fadeInDuration;
    public float fadeOutDuration;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        canvasGroup = FindFirstObjectByType<CanvasGroup>();
    }

    private async Task Fade(float start, float end, float duration)
    {
        float timer = 0f;

        if (canvasGroup == null) return;

        canvasGroup.alpha = start;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float t = timer / duration;
            float easedT = t * t;
            canvasGroup.alpha = Mathf.Lerp(start, end, easedT);
            await Task.Yield();
        }

        canvasGroup.alpha = end;
    }

    public async Task FadeIn() => await Fade(0f, 1f, fadeInDuration);
    public async Task FadeOut() => await Fade(1f, 0f, fadeOutDuration);

    // ====== TESTING ======

    [ContextMenu("Test FadeIn")]
    private void TestFadeIn()
    {
        _ = Instance.Fade(0f, 1f, fadeInDuration); // Execute fade in
    }

    [ContextMenu("Test FadeOut")]
    private void TestFadeOut()
    {
        _ = Instance.Fade(1f, 0f, fadeOutDuration); // Execute fade out
    }
}
