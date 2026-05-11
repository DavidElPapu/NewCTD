using UnityEngine;

public class IngredientScript : MonoBehaviour, ICookingObject
{
    public IngredientSO data;
    //For now thats all, hence the no MonoBehaviour use, but it might come later when adding the GUI for the ingredient icon

    public CookingObjectType GetCookingObjectType()
    {
        return CookingObjectType.Ingredient;
    }
}
