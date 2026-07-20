using UnityEngine;

public abstract class StationScript : MonoBehaviour
{
    public abstract bool TryPlaceItem(CookingObject item);
    public abstract CookingObject TryGetItem();
    public abstract void UseStation(CookingObject item);
}
