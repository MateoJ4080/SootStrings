using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    public enum Days { Day1, Day2, Day3, Day4, Day5 }
    private Days currentDay;
    public Days CurrentDay => currentDay;

    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;

    // Event that returns Task so subscribers can perform async operations
    public event Func<Days, Task> OnDayChangedAsync;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public async Task ChangeDayAsync(Days newDay)
    {
        // Fade out (can be awaited if it’s also async)
        await Fade(0f, 1f, fadeInDuration);

        currentDay = newDay;

        if (OnDayChangedAsync != null)
        {
            var invocationList = OnDayChangedAsync.GetInvocationList(); // Return array of delegates which work as a pointer to their method
            var tasks = new List<Task>();
            foreach (Func<Days, Task> handler in invocationList.Cast<Func<Days, Task>>())
            {
                try
                {
                    tasks.Add(handler.Invoke(newDay));
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error invoking OnDayChangedAsync: {e}");
                }
            }
            await Task.WhenAll(tasks); // Wait for all tasks (methods) to finish
        }

        // Fade in
        await Fade(1f, 0f, fadeOutDuration);
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

    // ====== TESTING ======

    [ContextMenu("Test FadeIn")]
    private void TestFadeIn()
    {
        _ = Fade(0f, 1f, fadeInDuration); // Ejecutar fade out
    }

    [ContextMenu("Test FadeOut")]
    private void TestFadeOut()
    {
        _ = Fade(1f, 0f, fadeOutDuration); // Ejecutar fade in
    }

    [ContextMenu("Test ChangeDay")]
    public async Task TestChangeDay()
    {
        await ChangeDayAsync(Days.Day1);
    }
}
