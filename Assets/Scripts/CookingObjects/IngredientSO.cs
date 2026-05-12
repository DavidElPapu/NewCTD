using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Create Ingredient SO")]
public class IngredientSO : ScriptableObject
{
    public CookingObjectName iName;
    public IngredientType type;
    public IngredientState state;
    public Mesh mesh;
    public Material material;
    public Vector3 customSize;
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
