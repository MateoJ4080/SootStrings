using UnityEngine;

[CreateAssetMenu(fileName = "DebugConfig", menuName = "Debug/DebugConfig", order = 1)]
public class DebugConfig : ScriptableObject
{
    public bool showDialogs = true;
    public bool autoStartDays = true;
}
