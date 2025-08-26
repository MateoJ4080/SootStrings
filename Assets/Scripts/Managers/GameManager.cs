using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { MainMenu, Loading, Playing, GameOver }
    public GameState CurrentState { get; private set; }

    public bool debugMode = true;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        GoToMainMenu();
    }

    public void GoToMainMenu()
    {
        CurrentState = GameState.MainMenu;
        // Logic
    }

    public void StartLoading()
    {
        CurrentState = GameState.Loading;
        // Logic
    }

    public void StartGame()
    {
        CurrentState = GameState.Playing;
        // Logic
    }

    public void EndGame()
    {
        CurrentState = GameState.GameOver;
        // Logic
    }

    public bool IsGameOver() => CurrentState == GameState.GameOver;
    public bool IsPlaying() => CurrentState == GameState.Playing;

    public void DebugLog(string message)
    {
        if (debugMode)
            Debug.Log(message);
    }
}