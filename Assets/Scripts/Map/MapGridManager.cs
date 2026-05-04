using UnityEngine;
using UnityEngine.InputSystem;

public class MapGridManager : MonoBehaviour
{
    public static MapGridManager singleton;
    public MapGridData gridData;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
            gridData = new MapGridData();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
