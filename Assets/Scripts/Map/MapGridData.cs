using System;
using System.Collections.Generic;
using UnityEngine;

public class MapGridData
{
    Dictionary<Vector3Int, GameObject> objectsOnMap = new();

    public void AddObjectAt(Vector3Int gridPosition, GameObject gameObject)
    {
        if (!objectsOnMap.ContainsKey(gridPosition))
            objectsOnMap[gridPosition] = gameObject;
    }

    public void RemoveObjectAt(Vector3Int gridPosition)
    {
        if (objectsOnMap.ContainsKey(gridPosition))
            objectsOnMap.Remove(gridPosition);
    }

    public GameObject GetGameObjectAt(Vector3Int gridPosition)
    {
        if (objectsOnMap.ContainsKey(gridPosition) == false)
            return null;
        return objectsOnMap[gridPosition].gameObject;
    }
}
