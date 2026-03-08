using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    [SerializeField] private List<MissionInstance> activeMissions;
    [SerializeField] private List<MissionInstance> completedMissions;

    private void OnEnable()
    {
        GameEvents.MissionCompleted += HandleMissionCompleted;
    }

    private void OnDisable()
    {
        GameEvents.MissionCompleted -= HandleMissionCompleted;
    }

    void HandleMissionCompleted(MissionInstance mission)
    {
        activeMissions.Remove(mission);
        completedMissions.Add(mission);
    }

    // Not useful yet because the game is lineal but keep as an example
    // void HandleShowerTaken()
    // {
    //     foreach (var mission in activeMissions)
    //         mission.OnShowerTaken();
    // }

    // public void UnlockMission(MissionInstance mission)
    // {
    //     if (!lockedMissions.Contains(mission)) return;

    //     lockedMissions.Remove(mission);
    //     activeMissions.Add(mission);
    // }
}
