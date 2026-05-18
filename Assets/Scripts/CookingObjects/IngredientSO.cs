using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Create Ingredient SO")]
public class IngredientSO : ScriptableObject
{
    public CookingObjectName iName;
    public IngredientType type;
    public Mesh mesh;
    public Material material;
    public Vector3 customSize;
    public Vector3 customOffset;
    public Color baseColor;
}

public enum IngredientType
{
    Fruit,
}

public enum IngredientState
{
    Intact,
    Cut,
    Blended,
    Cooked,
    Brewed,
    Squashed
}
