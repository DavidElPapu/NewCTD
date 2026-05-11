using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MapGridManager : MonoBehaviour
{
    public static MapGridManager singleton;
    [SerializeField] private Grid mapGrid;
    private Dictionary<Vector3Int, GameObject> objectsOnMap;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            objectsOnMap = new Dictionary<Vector3Int, GameObject>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddObjectAt(Vector3 position, GameObject gameObject)
    {
        Vector3Int gridPosition = GetGridPosition(position);
        if (!objectsOnMap.ContainsKey(gridPosition))
        {
            objectsOnMap[gridPosition] = gameObject;
            gameObject.transform.position = mapGrid.CellToWorld(gridPosition);
        }
    }

    public void RemoveObjectAt(Vector3 position)
    {
        Vector3Int gridPosition = GetGridPosition(position);
        if (objectsOnMap.ContainsKey(gridPosition))
            objectsOnMap.Remove(gridPosition);
    }

    public GameObject GetGameObjectAt(Vector3 position)
    {
        Vector3Int gridPosition = GetGridPosition(position);
        if (objectsOnMap.ContainsKey(gridPosition) == false)
            return null;
        return objectsOnMap[gridPosition].gameObject;
    }

    public Vector3Int GetGridPosition(Vector3 position)
    {
        return mapGrid.WorldToCell(position);
    }
}
