using System.Collections;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Popups General")]
    [SerializeField] private GameObject popupPrefab;
    [SerializeField] private string testMessage = "Test"; // For editor testing purposes only
    [SerializeField] private float popupDuration = 1.5f;
    [SerializeField] private float letterInterval = 0.1f;
    [SerializeField] private float wordInterval = 0.2f;
    private GameObject popup;
    private TextMeshProUGUI popupTMP;

    [Header("Popups Audio")]
    [SerializeField] private AudioSource popupAudioSource;
    [SerializeField] private AudioClip popupSound;
    [SerializeField] private AudioClip letterSound;

    [Header("Popup Fade Effect")]
    [SerializeField] private float fadeDuration = 0.5f;
    private CanvasGroup popupCanvasGroup;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private IEnumerator ShowPopupSequence(string message)
    {

        popup = Instantiate(popupPrefab, transform);
        popupTMP = GetComponentInChildren<TextMeshProUGUI>();
        popupCanvasGroup = popup.GetComponent<CanvasGroup>();

        if (popupPrefab == null || popupCanvasGroup == null || popupTMP == null)
        {
            Debug.LogError("UIManager: Missing references for popup components.");
            yield break;
        }

        yield return StartCoroutine(Fade(popupCanvasGroup, 0f, 1f, fadeDuration)); // Fade in
        yield return StartCoroutine(WriteTextPopup(message, popupTMP, popupAudioSource, letterSound));
        yield return new WaitForSeconds(popupDuration); // Let the popup stay visible for the specified duration
        yield return StartCoroutine(Fade(popupCanvasGroup, 1f, 0f, fadeDuration)); // Fade out
        Destroy(popup);
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

    private IEnumerator WriteTextPopup(string text, TMP_Text label, AudioSource audioSource, AudioClip sound)
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
        StartCoroutine(ShowPopupSequence(testMessage));
    }
}
