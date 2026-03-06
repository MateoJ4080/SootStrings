using UnityEngine;

// This SO helps organizing dialogues. Each event can have many dialogues or none at all.
// Unused for now. May be useful later, but consider alternatives to avoid extra classes and abstraction.
[CreateAssetMenu(fileName = "DialogueContainer", menuName = "Dialogue/DialogueContainer")]
public class DialogueContainer : ScriptableObject
{
    public DialogueData[] dialogues;
}