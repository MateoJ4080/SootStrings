using UnityEngine;

[CreateAssetMenu(fileName = "PopupData", menuName = "Scriptable Objects/PopupData")]
public class PopupData : ScriptableObject
{
    [SerializeField] private string text;
}
