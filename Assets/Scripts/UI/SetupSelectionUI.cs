using UnityEngine;

public class SetupSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject basicUI, readyUI;

    public void ShowUI()
    {
        basicUI.SetActive(true);
    }

    public void ShowReadyButton()
    {
        readyUI.SetActive(true);
    }

    public void HideUI()
    {
        basicUI.SetActive(false);
        readyUI.SetActive(false);
    }
}
