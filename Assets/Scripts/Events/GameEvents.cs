using System;

// Centralized events
public class GameEvents
{
    public static event Action<MissionInstance> MissionCompleted;

    public static event Action OnShowerTaken;
    public static event Action OnSlept;
    public static event Action OnPhoneTaken;

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
}
