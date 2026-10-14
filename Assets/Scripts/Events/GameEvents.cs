using System;
using UnityEngine;

// Centralized events
public class GameEvents
{
    public static event Action<MissionInstance> MissionCompleted;

    public static event Action OnShowerTaken;
    public static event Action OnSlept;
    public static event Action OnBrokenCellphoneTaken;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    private static void ResetEvents()
    {
        MissionCompleted = null;
        OnShowerTaken = null;
        OnSlept = null;
        OnBrokenCellphoneTaken = null;
    }

    public static void RaiseMissionCompleted(MissionInstance mission)
    {
        MissionCompleted?.Invoke(mission);
    }

    public static void RaiseOnShowerTaken()
    {
        OnShowerTaken?.Invoke();
    }

    public static void RaiseOnSlept()
    {
        OnSlept?.Invoke();
    }

    public static void RaiseOnBrokenCellphoneTaken()
    {
        OnBrokenCellphoneTaken?.Invoke();
    }
}
