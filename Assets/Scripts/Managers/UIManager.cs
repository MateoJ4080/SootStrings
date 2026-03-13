using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Settings")]
    [SerializeField] DebugConfig debugConfig;

    [Header("General")]
    [SerializeField] private float letterInterval = 0.1f;
    [SerializeField] private float wordInterval = 0.1f;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Interactable")]
    [SerializeField] private TMP_Text interactText;

    [Header("Audio")]
    [SerializeField] private AudioClip popupSound;
    [SerializeField] private AudioClip letterSound;

    [Header("Popups")]
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private float popupDuration = 1.5f;

    [Header("Dialogues")]
    [SerializeField] private GameObject dialoguePrefab;
    [SerializeField] private float dialogueDuration = 1.5f; // Dialogue should be skippable instead of having a fixed duration. Change this later.

    [Header("Objectives")]
    [SerializeField] private TMP_Text objectiveText;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator ShowMessage(Sprite background, float duration, string message)
    {
        if (dialoguePrefab == null)
        {
            Debug.LogError("UIManager: Prefab not assigned in the inspector.");
            yield break;
        }

        GameObject instance = Instantiate(dialoguePrefab, transform);
        CanvasGroup canvasGroup = instance.GetComponent<CanvasGroup>();
        TextMeshProUGUI tmp = instance.GetComponentInChildren<TextMeshProUGUI>();

        if (canvasGroup == null || tmp == null)
        {
            Debug.LogError("UIManager: CanvasGroup or TextMeshProUGUI component not found on the prefab.");
            Destroy(instance);
            yield break;
        }

        yield return StartCoroutine(Fade(canvasGroup, 0f, 1f, fadeDuration));
        yield return StartCoroutine(WriteTextUI(message, tmp, letterSound));
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(Fade(canvasGroup, 1f, 0f, fadeDuration));
        Destroy(instance);
    }

    public IEnumerator ShowPopup(string text)
    {
        if (!debugConfig.showDialogs) yield break;

        yield return ShowMessage(null, popupDuration, text);
    }

    public IEnumerator ShowDialogue(DialogueData dialogue)
    {
        if (!debugConfig.showDialogs) yield break;

        yield return ShowMessage(dialogue.Background, dialogue.Duration, dialogue.Text);
    }

    public void ShowInteractableText()
    {
        if (!interactText.gameObject.activeSelf)
            interactText.gameObject.SetActive(true);
    }

    public void HideInteractableText()
    {
        if (interactText.gameObject.activeSelf)
            interactText.gameObject.SetActive(false);
    }

    public void ShowObjectiveText(string text)
    {
        objectiveText.text = text;
        objectiveText.gameObject.SetActive(true);
    }

    public void HideObjectiveText()
    {
        if (!debugConfig.showObjective) return;

        objectiveText.gameObject.SetActive(false);
    }

    private IEnumerator WriteTextUI(string text, TMP_Text label, AudioClip sound)
    {
        label.text = "";
        foreach (char c in text)
        {
            label.text += c;

            if (AudioManager.Instance != null && sound != null)
                AudioManager.Instance.PlaySFX(sound);

            float waitTime = (c == ' ') ? wordInterval : letterInterval;
            yield return new WaitForSeconds(waitTime);
        }
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float start, float end, float duration)
    {
        float t = 0;
        canvasGroup.alpha = start;
        while (t < duration)
        {
            t += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, t / duration);
            yield return null;
        }
        canvasGroup.alpha = end;
    }

    // To test: right click on the UIManager in the Inspector and select "Test Popup". Use while in play mode to work as expected.
    // [ContextMenu("Test Popup")]
    // private void TestPopup()
    // {
    //     Debug.Log("Testing Popup");

    //     StopAllCoroutines();
    //     string text = "This is a test popup message";
    //     StartCoroutine(ShowPopup(text));
    // }

    // // To test: right click on the UIManager in the Inspector and select "Test Dialogue". Use while in play mode to work as expected.
    // [ContextMenu("Test Dialogue")]
    // private void TestDialogue()
    // {
    //     Debug.Log("Testing Dialogue");

    //     StopAllCoroutines();
    //     string text = "This is a test dialogue message";
    //     StartCoroutine(ShowDialogue(text));
    // }
}
