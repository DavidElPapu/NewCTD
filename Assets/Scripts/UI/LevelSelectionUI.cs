using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class LevelSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject levelSelectionPanel;
    [SerializeField] private Transform levelButtonsParent;
    [SerializeField] private Sprite unlockedLevelSprite, lockedLevelSprite;

    private void Start()
    {
        for (int i = 0; i < levelButtonsParent.childCount; i++)
        {
            Button button = levelButtonsParent.GetChild(i).gameObject.GetComponent<Button>();
            Image buttonImage = levelButtonsParent.GetChild(i).gameObject.GetComponent<Image>();

            if (i < ProgressionManager.Instance.UnlockedLevels)
            {
                button.interactable = true;
                buttonImage.sprite = unlockedLevelSprite;
            }
            else
            {
                button.interactable = false;
                buttonImage.sprite = lockedLevelSprite;
            }
        }
    }

    public void ToggleLevelSelectionVisibility(bool showMenu)
    {
        levelSelectionPanel.SetActive(showMenu);
    }

    public void SelectLevel(int levelSceneIndex)
    {
        SceneManager.LoadSceneAsync(levelSceneIndex, LoadSceneMode.Single);
    }
}
