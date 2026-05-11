using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Create Ingredient SO")]
public class IngredientSO : ScriptableObject
{
    public IngredientName iName;
    public IngredientType type;
    public IngredientState state;
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
