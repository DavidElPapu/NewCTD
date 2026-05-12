using UnityEngine;

public interface ICookingObject
{
    CookingObjectType GetCookingObjectType();
    CookingObjectName GetCookingObjectName();
}

public enum CookingObjectType
{
    Ingredient,
    Container,
    Tool
}

public enum CookingObjectName
{
    Coconut,
    Pineapple,
    Glass,
    Pot
}
