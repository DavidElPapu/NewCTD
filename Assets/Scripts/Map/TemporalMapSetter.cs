using UnityEngine;
using System.Collections.Generic;

public class TemporalMapSetter : MonoBehaviour
{
    public List<GameObject> stationsOnMap;
    public List<CookingObject> itemsOnMap;

    private void Start()
    {
        foreach (GameObject station in stationsOnMap)
        {
            MapGridManager.singleton.AddObjectAt(station.transform.position, station);
        }
        foreach (CookingObject item in itemsOnMap)
        {
            GameObject possibleStation = MapGridManager.singleton.GetGameObjectAt(item.transform.position);
            if (possibleStation != null && possibleStation.TryGetComponent(out IPlaceable stationInteraction))
            {
                if (stationInteraction.CanPlaceItem(item))
                {
                    //no if since we assume we placed containers where we know is valid
                }
            }
        }
    }
}
