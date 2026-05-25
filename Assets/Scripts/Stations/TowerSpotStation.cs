using UnityEngine;

[RequireComponent(typeof(UsableStation))]
public class TowerSpotStation : MonoBehaviour, IPlaceable
{
    private UsableStation usableStation;
    private GameObject currentTower;

    private void Awake()
    {
        TryGetComponent(out usableStation);
        currentTower = null;
    }

    private void OnEnable()
    {
        usableStation.OnStationUse += OnUse;
    }

    private void OnDisable()
    {
        usableStation.OnStationUse -= OnUse;
    }

    public bool CanPlaceItem(GameObject item)
    {
        if (item.TryGetComponent(out TowerPlacer towerContainer) && towerContainer.towerPrefab != null)
        {
            if (currentTower == null)
            {
                GameObject newTower = Instantiate(towerContainer.towerPrefab, transform.position, transform.rotation);
                currentTower = newTower;
                if (item.TryGetComponent(out ContainerScript container))
                    container.EmptyContainer();
            }
            //else if (checar que sean la misma torre para mejorar)
        }
        return false;
    }

    public GameObject OnPickEmpty()
    {
        return null;
    }

    private void OnUse(GameObject usedItem)
    {
        //For now, this is just used to delete the tower, maybe later could be more uses for diferent tools on towers
        //Before destroying could call a function of the tower for closure
        Destroy(currentTower);
        currentTower = null;
    }
}
