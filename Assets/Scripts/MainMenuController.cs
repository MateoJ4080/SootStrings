using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("MainMenu")]
    [SerializeField] private GameObject MainPanel;

    [Header("SettingsMenu")]
    [SerializeField] private GameObject SettingsPanel;

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void ShowMainPanel()
    {
        SettingsPanel.SetActive(false);
        MainPanel.SetActive(true);
    }

    public void ShowSettingsPanel()
    {
        MainPanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }
}
