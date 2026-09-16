using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private LevelSelectionUI levelSelectionUI;
    [SerializeField] private GameObject mainMenuPanel;

    public void ToggleMainMenuVisibility(bool showMenu)
    {
        mainMenuPanel.SetActive(showMenu);
    }

    public void OpenLevelSelectionMenu()
    {
        levelSelectionUI.ToggleLevelSelectionVisibility(true);
        ToggleMainMenuVisibility(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
