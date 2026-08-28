using UnityEngine;

public class ItemProvider : StationScript
{
    public IngredientSO ingredientSO;
    [SerializeField] private GameObject ingredientPrefab;
    private IngredientState initialState;

    public void SetIngredient(IngredientSO ingredient, IngredientState state)
    {
        ingredientSO = ingredient;
        initialState = state;
    }

    public override CookingObject TryGetItem()
    {
        GameObject newIngredient = Instantiate(ingredientPrefab);
        if (newIngredient.TryGetComponent(out IngredientScript ingredientScript))
        {
            ingredientScript.cName = ingredientSO.iName;
            ingredientScript.icon = ingredientSO.icon;
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
