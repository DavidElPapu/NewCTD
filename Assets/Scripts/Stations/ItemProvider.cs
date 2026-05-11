using UnityEngine;

public class ItemProvider : MonoBehaviour, IPlaceable
{
    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private IngredientSO ingredientSO;

    public void SetIngredient(IngredientSO ingredient)
    {
        ingredientSO = ingredient;
    }

    public bool CanPlaceItem(GameObject item)
    {
        return false;
    }

    public GameObject OnPickEmpty()
    {
        GameObject newIngredient = Object.Instantiate(ingredientPrefab);
        if (newIngredient.TryGetComponent(out IngredientScript ingredientScript))
        {
            ingredientScript.data = ingredientSO;
        }
        else
            return null;
        return newIngredient;
    }
}
