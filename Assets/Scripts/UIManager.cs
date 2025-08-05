using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("General")]
    [SerializeField] private float letterInterval = 0.1f;
    [SerializeField] private float wordInterval = 0.1f;
    [SerializeField] private float fadeDuration = 0.5f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip popupSound;
    [SerializeField] private AudioClip letterSound;

    [Header("Popups")]
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private float popupDuration = 1.5f;

    [Header("Dialogues")]
    [SerializeField] private GameObject dialoguePrefab;
    [SerializeField] private float dialogueDuration = 1.5f; // Dialogue should be skippable instead of having a fixed duration. Change this later.

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator ShowMessage(GameObject prefab, float duration, string message)
    {
        if (prefab == null)
        {
            Debug.LogError("UIManager: Prefab not assigned in the inspector.");
            yield break;
        }

        GameObject instance = Instantiate(prefab, transform);
        CanvasGroup canvasGroup = instance.GetComponent<CanvasGroup>();
        TextMeshProUGUI tmp = instance.GetComponentInChildren<TextMeshProUGUI>();

        if (canvasGroup == null || tmp == null)
        {
            Debug.LogError("UIManager: CanvasGroup or TextMeshProUGUI component not found on the prefab.");
            Destroy(instance);
            yield break;
        }

        yield return StartCoroutine(Fade(canvasGroup, 0f, 1f, fadeDuration));
        yield return StartCoroutine(WriteTextUI(message, tmp, audioSource, letterSound));
        yield return new WaitForSeconds(duration);
        yield return StartCoroutine(Fade(canvasGroup, 1f, 0f, fadeDuration));
        Destroy(instance);
    }

    public IEnumerator ShowPopup(UIMessageData data)
    {
        yield return ShowMessage(popupPrefab, popupDuration, data.messageText);
    }

    public IEnumerator ShowDialogue(DialogueData data)
    {
        yield return ShowMessage(dialoguePrefab, dialogueDuration, data.messageText);
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

    private IEnumerator WriteTextUI(string text, TMP_Text label, AudioSource audioSource, AudioClip sound)
    {
        label.text = "";
        foreach (char c in text)
        {
            label.text += c;

            if (audioSource != null && sound != null)
                audioSource.PlayOneShot(sound);

            float waitTime = (c == ' ') ? wordInterval : letterInterval;
            yield return new WaitForSeconds(waitTime);
        }
    }

    // To test: right click on the UIManager in the Inspector and select "Test Popup". Use while in play mode to work as expected.
    [ContextMenu("Test Popup")]
    private void TestPopup()
    {
        Debug.Log("Testing Popup");

        StopAllCoroutines();
        PopupData popupData = ScriptableObject.CreateInstance<PopupData>();
        popupData.messageText = "This is a test popup message";
        StartCoroutine(ShowPopup(popupData));
    }

    // To test: right click on the UIManager in the Inspector and select "Test Dialogue". Use while in play mode to work as expected.
    [ContextMenu("Test Dialogue")]
    private void TestDialogue()
    {
        Debug.Log("Testing Dialogue");

        StopAllCoroutines();
        DialogueData dialogueData = ScriptableObject.CreateInstance<DialogueData>();
        dialogueData.messageText = "This is a test dialogue message";
        StartCoroutine(ShowDialogue(dialogueData));
    }
}
