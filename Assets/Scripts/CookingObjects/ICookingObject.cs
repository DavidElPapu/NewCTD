using UnityEngine;

public interface ICookingObject
{
    CookingObjectType GetCookingObjectType();
}

public enum CookingObjectType
{
    Ingredient,
    Container
}
