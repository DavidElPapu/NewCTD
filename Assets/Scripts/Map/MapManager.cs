using System;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public event Action MapSetupDone;
    public List<StationScript> levelStations;
    public List<ToolScript> levelTools;
    [SerializeField] private Transform startingStationsParent, startingItemsParent;

    public void SetMapStationsAndItems()
    {
        levelStations = new List<StationScript>();
        levelTools = new List<ToolScript>();

        //Sets the stations in the map grid manager and enables the slectable stations
        foreach (Transform stationTransform in startingStationsParent)
        {
            if (stationTransform.TryGetComponent(out StationScript station))
            {
                levelStations.Add(station);
                MapGridManager.singleton.AddStationAt(station.transform.position, station);
                SetupSelectable setupSelectable = stationTransform.GetComponentInChildren<SetupSelectable>(true);
                if (setupSelectable != null)
                    LevelManager.singleton.setupSelectionManager.AddMapSelectable(setupSelectable);
            }
        }

        //Places the items inside the stations they are on (uses a reverse for because childs are modified)
        for (int i = startingItemsParent.childCount - 1; i >= 0; i--)
        {
            if (startingItemsParent.GetChild(i).TryGetComponent(out CookingObject item))
            {
                if (item is ToolScript toolScript)
                    levelTools.Add(toolScript);
                StationScript possibleStation = MapGridManager.singleton.GetStationAt(item.transform.position);
                if (possibleStation != null)
                {
                    if (!possibleStation.TryPlaceItem(item))
                    {
                        Debug.LogError(item.gameObject.name + " couldn't be placed in " + possibleStation.gameObject.name);
                    }
                }
            }
        }

        MapSetupDone?.Invoke();
    }
}
