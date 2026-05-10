using UnityEngine;

public class IngredientScript : ICookingObject
{
    public IngredientName name;
    public IngredientType type;
    public IngredientState state;

    //For now thats all, hence the no MonoBehaviour use, but it might come later when adding the GUI for the ingredient icon

    public CookingObjectType GetCookingObjectType()
    {
        return CookingObjectType.Ingredient;
    }
}

public enum IngredientName
{
    Coconut,
    Pinapple
}

public enum IngredientType
{
    Fruit,
}

public enum IngredientState
{
    Intact,
    Cut,
    Blended
}
