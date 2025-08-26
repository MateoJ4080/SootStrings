using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    public enum Days { Day1, Day2, Day3, Day4, Day5 }
    private Days currentDay = Days.Day1;
    public Days CurrentDay => currentDay;

    [SerializeField] private CanvasGroup canvasGroup;

    // Event that handle methods who recieve a parameter of type Days and return a Task
    public event Func<Days, Task> OnDayChangedAsync;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    // Changes day and call every method subscribed to OnDayChangedAsync
    public async Task ChangeDayAsync(Days newDay)
    {
        Debug.Log("ChangeDayAsync");

        // Fade in
        await FadeManager.Instance.FadeIn();

        currentDay = newDay;

        if (OnDayChangedAsync != null)
        {
            var invocationList = OnDayChangedAsync.GetInvocationList(); // Return array of delegates which work as a pointer to their methods
            var tasks = new List<Task>();
            foreach (Func<Days, Task> method in invocationList.Cast<Func<Days, Task>>())
            {
                try
                {
                    tasks.Add(method.Invoke(newDay)); // Invokes all tasks assigned to the 
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error invoking OnDayChangedAsync: {e}");
                }
            }
            await Task.WhenAll(tasks); // Wait for all tasks (methods) to finish before the fade out
        }

        // Fade out
        await FadeManager.Instance.FadeOut();
    }

    // ====== TESTING ======

    [ContextMenu("Test ChangeDay")]
    public async Task TestChangeDay()
    {
        await ChangeDayAsync(Days.Day1);
    }
}
