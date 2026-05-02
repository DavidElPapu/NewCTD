using UnityEngine;

public abstract class CookingObjectSO : ScriptableObject
{
    public string objectName;
    public ObjectType type;
}
public enum ObjectType
{
    Ingredient,
    Station,
    Container,
    Tool,
    Tower
}
