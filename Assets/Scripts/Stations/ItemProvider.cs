using UnityEngine;

public class ItemProvider : MonoBehaviour, IPlaceable
{
    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private IngredientSO ingredientSO;
    [SerializeField] private IngredientState initialState;

    public void SetIngredient(IngredientSO ingredient)
    {
        ingredientSO = ingredient;
    }

    public bool CanPlaceItem(CookingObject item)
    {
        return false;
    }

    public CookingObject OnPickEmpty()
    {
        GameObject newIngredient = Instantiate(ingredientPrefab);
        if (newIngredient.TryGetComponent(out IngredientScript ingredientScript))
        {
            ingredientScript.data = ingredientSO;
            ingredientScript.state = initialState;
            ingredientScript.ChangeMesh(ingredientSO.mesh);
            ingredientScript.ChangeMaterial(ingredientSO.material);
            ingredientScript.ChangeSizeAndOffset(ingredientSO.customSize, ingredientSO.customOffset);
        }
        else
            return null;
        return ingredientScript;
    }
}
