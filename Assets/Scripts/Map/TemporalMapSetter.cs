using UnityEngine;
using System.Collections.Generic;

public class TemporalMapSetter : MonoBehaviour
{
    public List<StationScript> mapStations;
    public List<CookingObject> mapItems;

    private void Start()
    {
        foreach (StationScript station in mapStations)
        {
            MapGridManager.singleton.AddStationAt(station.transform.position, station);
        }
        foreach (CookingObject item in mapItems)
        {
            StationScript possibleStation = MapGridManager.singleton.GetStationAt(item.transform.position);
            if (possibleStation != null)
            {
                if (possibleStation.TryPlaceItem(item))
                {
                    //no if since we assume we placed containers where we know is valid
                }
            }
        }
    }
}
