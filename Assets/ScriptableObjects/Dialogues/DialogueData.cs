using UnityEngine;

public class DialogueData : ScriptableObject
{
    [SerializeField] private string text;
    [SerializeField] private Sprite background;
    [SerializeField] private float duration;
}