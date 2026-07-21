using UnityEngine;

public class ItemProvider : StationScript
{
    [SerializeField] private GameObject ingredientPrefab;
    [SerializeField] private IngredientSO ingredientSO;
    [SerializeField] private IngredientState initialState;

    public void SetIngredient(IngredientSO ingredient)
    {
        //This method will be used when selecting ingredients for the level
        ingredientSO = ingredient;
    }

    public override CookingObject TryGetItem()
    {
        GameObject newIngredient = Instantiate(ingredientPrefab);
        if (newIngredient.TryGetComponent(out IngredientScript ingredientScript))
        {
            ingredientScript.data = ingredientSO;
            ingredientScript.state = initialState;
            ingredientScript.ChangeMesh(ingredientSO.mesh);
            ingredientScript.ChangeMaterial(ingredientSO.material);
        }
        else
            return null;
        return ingredientScript;
    }

    public override bool TryPlaceItem(CookingObject item)
    {
        return false;
    }

    public override void UseStation(CookingObject item)
    {
        //Nothing
    }
}
