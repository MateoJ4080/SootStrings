using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Popup Data", menuName = "UI/Popup Data")]
public class PopupData : ScriptableObject
{
    [SerializeField] private string message;
}
