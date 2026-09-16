using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "CookingObjects/Create Ingredient SO")]
public class IngredientSO : ScriptableObject
{
    public int ID;
    public CookingObjectName iName;
    public Mesh mesh;
    public Material material;
    public Color baseColor;
    public Sprite icon;
}

public enum IngredientState
{
    Null,
    Intact,
    Sliced,
    Cut,
    Carbonated,
    Extracted,
    Blended,
    ReadyToDrink
}
