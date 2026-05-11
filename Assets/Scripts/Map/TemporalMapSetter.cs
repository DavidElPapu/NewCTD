using UnityEngine;
using System.Collections.Generic;

public class TemporalMapSetter : MonoBehaviour
{
    public List<GameObject> stationsOnMap;

    private void Start()
    {
        foreach (GameObject station in stationsOnMap)
        {
            MapGridManager.singleton.AddObjectAt(station.transform.position, station);
        }
    }
}
