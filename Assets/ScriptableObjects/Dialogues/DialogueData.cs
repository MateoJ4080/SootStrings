using UnityEngine;

[CreateAssetMenu(fileName = "DialogueData", menuName = "Dialogue/DialogueData")]
public class DialogueData : ScriptableObject
{
    [SerializeField] private string text;
    [SerializeField] private Sprite background;
    [SerializeField] private float duration;

    public string Text => text;
    public Sprite Background => background;
    public float Duration => duration;
}