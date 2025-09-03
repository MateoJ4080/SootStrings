using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInteractor playerInteractor;
    [SerializeField] private CanvasGroup canvasGroup;

    [Header("Day Events")]
    [SerializeField] private GameObject day1Events;
    [SerializeField] private GameObject day2Events;
    [SerializeField] private GameObject day3Events;
    [SerializeField] private GameObject day4Events;
    [SerializeField] private GameObject day5Events;

    public enum Days { Day1, Day2, Day3, Day4, Day5 }
    private Days currentDay = Days.Day1;
    public Days CurrentDay
    {
        get => currentDay;
        private set => currentDay = value;
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }


    public void Start()
    {
        SetPlayerActive(false);
        StartCoroutine(RunDay(currentDay));
    }


    private IEnumerator RunDay(Days day)
    {
        Debug.Log("StartDay " + day);

        yield return StartCoroutine(FadeManager.Instance.FadeOut());
        SetPlayerActive(true);

        // Execute day events
        DayEvent[] events = GetEventsForDay(day);
        foreach (DayEvent e in events)
        {
            if (e is ShowerEvent showerEvent)
            {
                var shower = FindFirstObjectByType<InteractableShower>();
                showerEvent.Initialize(shower);
            }

            yield return StartCoroutine(e.Execute());
        }

        yield return StartCoroutine(FadeManager.Instance.FadeIn());
        SetPlayerActive(false);

        // Go to next day only if there's one
        int nextDay = (int)day + 1;
        if (nextDay < Enum.GetValues(typeof(Days)).Length)
        {
            currentDay++;
            StartCoroutine(RunDay(currentDay));
        }
    }

    private DayEvent[] GetEventsForDay(Days day)
    {
        if (day1Events == null || day2Events == null || day3Events == null || day4Events == null || day5Events == null)
        {
            Debug.LogError("DayManager: One or more day event GameObjects are not assigned in the inspector.");
            return new DayEvent[0];
        }

        return day switch
        {
            Days.Day1 => day1Events.GetComponents<DayEvent>(),
            Days.Day2 => day2Events.GetComponents<DayEvent>(),
            Days.Day3 => day3Events.GetComponents<DayEvent>(),
            Days.Day4 => day4Events.GetComponents<DayEvent>(),
            Days.Day5 => day5Events.GetComponents<DayEvent>(),
            _ => new DayEvent[0],
        };
    }

    // Enable/disable player controls and interactor
    public void SetPlayerActive(bool active)
    {
        Debug.Log($"SetPlayerActive set to {active}");
        playerController.enabled = active;
        playerController.GravityEnabled = active;
        playerInteractor.enabled = active;
        UIManager.Instance.ShowInteractableText(false);
    }

    // ====== TESTING ======

    [ContextMenu("Test ChangeDay")]
    public void TestChangeDay()
    {
        StartCoroutine(ChangeDayEffect());
    }

    public IEnumerator ChangeDayEffect()
    {
        yield return StartCoroutine(FadeManager.Instance.FadeIn());
        yield return StartCoroutine(FadeManager.Instance.FadeOut());
    }
}
