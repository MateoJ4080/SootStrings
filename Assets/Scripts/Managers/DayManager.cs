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
    [SerializeField] private DayEvent[] day1Events;
    [SerializeField] private DayEvent[] day2Events;
    [SerializeField] private DayEvent[] day3Events;
    [SerializeField] private DayEvent[] day4Events;
    [SerializeField] private DayEvent[] day5Events;

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
        foreach (var e in events)
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
        switch (day)
        {
            case Days.Day1:
                return day1Events;

            case Days.Day2:
                return day2Events;

            case Days.Day3:
                return day3Events;

            case Days.Day4:
                return day4Events;

            case Days.Day5:
                return day5Events;

            default:
                return new DayEvent[0];
        }
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
