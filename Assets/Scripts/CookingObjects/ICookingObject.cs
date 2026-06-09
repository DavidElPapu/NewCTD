using UnityEngine;

public interface ICookingObject
{
    CookingObjectName GetCookingObjectName();
}

public enum CookingObjectName
{
    Stick,
    Coconut,
    Pineapple,
    Glass,
    Pot,
    Cauldron,
    ButcherKnife
}
