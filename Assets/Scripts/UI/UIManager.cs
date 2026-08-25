using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager singleton;
    private SetupSelectionUI setupSelectionUI;
    private IngredientSelectionUI ingredientSelectionUI;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            setupSelectionUI = GetComponentInChildren<SetupSelectionUI>(true);
            ingredientSelectionUI = GetComponentInChildren<IngredientSelectionUI>(true);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowUI(UIType uIType)
    {
        switch (uIType)
        {
            case UIType.SetupSelectionUI:
                setupSelectionUI.ShowUI();
                break;
            case UIType.SetupSelectionReadyUI:
                setupSelectionUI.ShowReadyButton();
                break;
            default:
                Debug.LogError("This UI Type requires an object reference to show");
                break;
        }
    }

    public void ShowObjectUI(UIType uIType, GameObject obj)
    {
        switch (uIType)
        {
            case UIType.IngredientSelectionUI:
                ingredientSelectionUI.ShowSelectionPanel(obj);
                break;
            default:
                Debug.LogError("This UI Type doesn't need an object reference");
                break;
        }
    }

    public void HideUI(UIType uIType)
    {
        switch (uIType)
        {
            case UIType.SetupSelectionUI:
                setupSelectionUI.HideUI();
                break;
            case UIType.IngredientSelectionUI:
                ingredientSelectionUI.HideSelectionPanel();
                break;
            default:
                break;
        }
    }
}

public enum UIType
{
    IngredientSelectionUI,
    SetupSelectionUI,
    SetupSelectionReadyUI,
}
