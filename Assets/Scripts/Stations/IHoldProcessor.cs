using UnityEngine;

public interface IHoldProcessor
{
    void OnItemEnter(CookingObject item);
    void OnItemExit(CookingObject item);
}
