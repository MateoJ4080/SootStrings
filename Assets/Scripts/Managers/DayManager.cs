using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class DayManager : MonoBehaviour
{
    public static DayManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] DebugConfig debugConfig;

    [Header("References")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private PlayerInteractor playerInteractor;
    [SerializeField] private CanvasGroup canvasGroup;

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

        // Debug: if in debugMode mode, enable all interactables for testing.
        if (debugConfig.debugModeEnabled)
        {
            playerController.gameObject.transform.position = new(-3f, 1.5f, -10f);

            var monos = FindObjectsByType<MonoBehaviour>(FindObjectsSortMode.None);
            var interactables = monos.OfType<IInteractable>();
            var dayEvents = monos.OfType<DayEvent>();

            foreach (IInteractable interactable in interactables)
            {
                interactable.Activate();
            }

            foreach (DayEvent dayEvent in dayEvents)
            {
                StartCoroutine(dayEvent.Execute());
            }
        }

    }


    private IEnumerator RunDay(Days day)
    {
        Debug.Log("StartDay " + day);

        yield return StartCoroutine(FadeManager.Instance.FadeOut());
        SetPlayerActive(true);

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

    // Enable/disable player controls and interactor
    public void SetPlayerActive(bool active)
    {
        Debug.Log($"SetPlayerActive set to {active}");
        playerController.enabled = active;
        playerController.GravityEnabled = active;
        playerInteractor.enabled = active;
        UIManager.Instance.HideInteractableText();
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
