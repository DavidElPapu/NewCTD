using UnityEngine;

public interface ICookingObject
{
    CookingObjectName GetCookingObjectName();
}

public enum CookingObjectName
{
    Null,
    Coconut,
    PassionFruit,
    Pineapple,
    IceCube,
    Starfruit,
    HabaneroPepper,
    Mango,
    Glass,
    CarbonatorBottle,
    BlenderPitcher,
    Cleaver,
    KitchenKnife
}
