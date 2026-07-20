using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class MapGridManager : MonoBehaviour
{
    public static MapGridManager singleton;
    [SerializeField] private Grid mapGrid;
    private Dictionary<Vector3Int, StationScript> stationsOnMap;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            stationsOnMap = new Dictionary<Vector3Int, StationScript>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddStationAt(Vector3 position, StationScript station)
    {
        Vector3Int gridPosition = GetGridPosition(position);
        if (!stationsOnMap.ContainsKey(gridPosition))
        {
            stationsOnMap[gridPosition] = station;
            station.transform.position = mapGrid.CellToWorld(gridPosition);
        }
    }

    public void RemoveObjectAt(Vector3 position)
    {
        Vector3Int gridPosition = GetGridPosition(position);
        if (stationsOnMap.ContainsKey(gridPosition))
            stationsOnMap.Remove(gridPosition);
    }

    public StationScript GetStationAt(Vector3 position)
    {
        Vector3Int gridPosition = GetGridPosition(position);
        if (stationsOnMap.ContainsKey(gridPosition) == false)
            return null;
        return stationsOnMap[gridPosition];
    }

    public Vector3Int GetGridPosition(Vector3 position)
    {
        return mapGrid.WorldToCell(position);
    }
}
