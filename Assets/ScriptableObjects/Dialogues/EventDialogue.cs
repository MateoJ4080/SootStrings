using UnityEngine;

// This scriptable object helps organizing dialogues for day events. Each event can have many dialogues or none at all.
[CreateAssetMenu(fileName = "EventDialogue", menuName = "DayEvents/EventDialogue")]
public class EventDialogue : ScriptableObject
{
    public string[] dialogues;
    public float delayBeforeDialogue;
}