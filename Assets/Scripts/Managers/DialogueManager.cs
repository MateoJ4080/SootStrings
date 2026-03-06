using System.Collections;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public IEnumerator PlaySequence(DialogueData[] dialogues)
    {
        foreach (DialogueData dialogue in dialogues)
        {
            yield return UIManager.Instance.ShowDialogue(dialogue);
        }
    }
}
