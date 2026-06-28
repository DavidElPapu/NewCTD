using UnityEngine;

[RequireComponent(typeof(UsableStation))]
public class TowerSpotStation : MonoBehaviour, IPlaceable, IUseProcessor
{
    private MainTowerController currentTower;

    private void Awake()
    {
        currentTower = null;
    }

    public bool CanPlaceItem(GameObject item)
    {
        if (item.TryGetComponent(out TowerPlacer towerContainer) && towerContainer.towerPrefab != null)
        {
            if (currentTower == null)
            {
                GameObject newTower = Instantiate(towerContainer.towerPrefab, transform.position, transform.rotation);
                currentTower = newTower.GetComponent<MainTowerController>();
                if (item.TryGetComponent(out ContainerScript container))
                    container.EmptyContainer();
            }
            else if (towerContainer.towerPrefab.TryGetComponent(out MainTowerController tower) && (tower.GetName() == currentTower.GetName()) && currentTower.CanUpgrade())
            {
                currentTower.UpgradeTower();
                if (item.TryGetComponent(out ContainerScript container2))
                    container2.EmptyContainer();
            }
        }
        return false;
    }

    public GameObject OnPickEmpty()
    {
        return null;
    }

    public void OnProcessorUse(GameObject usedItem)
    {
        //For now, this is just used to delete the tower, maybe later could be more uses for diferent tools on towers
        currentTower.DeleteTower();
        currentTower = null;
    }
}
