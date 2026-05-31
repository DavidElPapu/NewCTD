using UnityEngine;

public interface IHoldProcessor
{
    void OnItemEnter(GameObject item);
    void OnItemExit(GameObject item);
}
