using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientSelectionUI : MonoBehaviour
{
    [SerializeField] private List<IngredientSO> ingredientsSOs;
    [SerializeField] private Transform ingredientsButtonsParent;
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private Image selectedIngredientImage;
    [SerializeField] private Sprite unselectedIngredientSprite, lockedIngredientSprite;
    private ItemProvider targetProvider;

    private void Awake()
    {
        selectionPanel.SetActive(false);
        if (ProgressionManager.Instance != null)
        {
            //If there exists a progresion manager, loops through every ingredientSO to set the ingredient buttons
            for (int i = 0; i < ingredientsSOs.Count; i++)
            {
                //Gets the respective button and image of the ingredientSO (they match should match index
                if (!ingredientsButtonsParent.GetChild(i).TryGetComponent(out Button button)) return;
                if (!ingredientsButtonsParent.GetChild(i).TryGetComponent(out Image icon)) return;

                if (ProgressionManager.Instance.UnlockedIngredientsIDs.Contains(ingredientsSOs[i].ID))
                {
                    //If there thei ingredient is unlocked, assigns the icon and actrivates the button
                    icon.sprite = ingredientsSOs[i].icon;
                    button.interactable = true;
                }
                else
                {
                    icon.sprite = lockedIngredientSprite;
                    button.interactable = false;
                }
            }
        }
    }

    public void ShowSelectionPanel(GameObject selectableStation)
    {
        if (selectableStation.TryGetComponent(out targetProvider))
        {
            selectionPanel.SetActive(true);
            if (targetProvider.ingredientSO != null)
                selectedIngredientImage.sprite = targetProvider.ingredientSO.icon;
            else
                selectedIngredientImage.sprite = unselectedIngredientSprite;
        }
    }

    public void HideSelectionPanel()
    {
        targetProvider = null;
        selectionPanel.SetActive(false);
    }

    public void SetTargetIngredient(IngredientSO ingredient)
    {
        //This is a temporal if made for the icecubes to be ready, normally this if wouldn't exist
        if (ingredient.iName == CookingObjectName.IceCube)
            targetProvider.SetIngredient(ingredient, IngredientState.ReadyToDrink);
        else
            targetProvider.SetIngredient(ingredient, IngredientState.Intact);
        selectedIngredientImage.sprite = ingredient.icon;
    }

    public void SetSelectable()
    {
        if (targetProvider.ingredientSO != null)
            LevelManager.singleton.setupSelectionManager.OnSelectableSet();
    }
}
