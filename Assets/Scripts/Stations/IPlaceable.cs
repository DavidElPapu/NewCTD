using UnityEngine;

public interface IPlaceable
{
    GameObject OnPickEmpty();
    bool CanPlaceItem(GameObject item);
}
