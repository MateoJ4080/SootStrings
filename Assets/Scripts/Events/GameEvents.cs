using System;

// Centralized events
public class GameEvents
{
    public static event Action OnShowerTaken;
    public static event Action OnSlept;
    public static event Action OnPhoneTaken;

    public static void RaiseOnShowerTaken()
    {
        OnShowerTaken?.Invoke();
    }
}
