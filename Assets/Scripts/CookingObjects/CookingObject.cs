using UnityEngine;

public abstract class CookingObject : MonoBehaviour
{
    public CookingObjectName cName;
    public Sprite icon;
    public abstract bool TryEnterItem(CookingObject item);
    public abstract void OnPlayerInteraction(bool wasPicked);
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
