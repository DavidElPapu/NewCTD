using UnityEngine;

public class ToolScript : MonoBehaviour, ICookingObject
{
    public CookingObjectName tName;
    [Header("For processing")]
    public IngredientState processedState;

    public CookingObjectName GetCookingObjectName()
    {
        return tName;
    }
}
