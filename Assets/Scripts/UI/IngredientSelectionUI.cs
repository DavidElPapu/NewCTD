using UnityEngine;
using UnityEngine.UI;

public class IngredientSelectionUI : MonoBehaviour
{
    [SerializeField] private GameObject selectionPanel;
    [SerializeField] private Image selectedIngredientImage;
    [SerializeField] private Sprite unselectedIngredientSprite;
    private ItemProvider targetProvider;

    private void Awake()
    {
        selectionPanel.SetActive(false);
    }

    public void ShowSelectionPanel(GameObject target)
    {
        if (target.TryGetComponent(out targetProvider))
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
