using UnityEngine;
using System.Collections.Generic;

public class TemporalMapSetter : MonoBehaviour
{
    public List<GameObject> stationsOnMap;
    public List<GameObject> containersOnMap;

    private void Start()
    {
        foreach (GameObject station in stationsOnMap)
        {
            MapGridManager.singleton.AddObjectAt(station.transform.position, station);
        }
        foreach (GameObject container in containersOnMap)
        {
            GameObject possibleStation = MapGridManager.singleton.GetGameObjectAt(container.transform.position);
            if (possibleStation != null && possibleStation.TryGetComponent(out IPlaceable stationInteraction))
            {
                if (stationInteraction.CanPlaceItem(container))
                {
                    //no if since we assume we placed containers where we know is valid
                }
            }
        }
    }
}
