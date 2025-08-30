using UnityEngine;

// In desuse for now, may be useful later if we want to change the used dialogues from the inspector

// This scriptable object helps organizing dialogues for day events. Each event can have many dialogues or none at all.
[CreateAssetMenu(fileName = "EventDialogue", menuName = "DayEvents/EventDialogue")]
public class EventDialogue : ScriptableObject
{
    public string[] dialogues;
    public float delayBeforeDialogue;
}