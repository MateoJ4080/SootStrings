using UnityEngine;

[CreateAssetMenu(fileName = "DebugConfig", menuName = "Debug/DebugConfig", order = 1)]
public class DebugConfig : ScriptableObject
{
    public bool debugModeEnabled = true;
    public bool showDialogues = true;
    public bool showObjective = true;
}
