using UnityEngine;

public interface IInteractable
{
    GameObject OnPickEmpty();
    bool CanPlaceItem(GameObject item);
    void OnUse();
}
