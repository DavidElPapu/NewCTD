using UnityEngine;

public interface IPlaceable
{
    CookingObject OnPickEmpty();
    bool CanPlaceItem(CookingObject item);
}
