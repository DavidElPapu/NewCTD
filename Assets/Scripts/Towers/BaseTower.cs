using UnityEngine;
using System.Collections.Generic;

public class BaseTower : MonoBehaviour
{
    [SerializeField] protected List<BaseTowerSO> levelsData;
    [SerializeField] private List<GameObject> levelsModels;
    private TowerContext context;
    private int currentLevel = 0;

    protected virtual void Awake()
    {
        for (int i = 0; i < levelsModels.Count; i++)
        {
            if (i != currentLevel)
                levelsModels[i].SetActive(false);
            else
                levelsModels[i].SetActive(true);
        }
        context = new TowerContext
        (
            gameObject,
            this,
            GetComponent<TowerDetectionRange>()
        );
    }

    public virtual bool CanUpgrade()
    {
        if (currentLevel < levelsModels.Count - 1) return true;
        return false;
    }

    public virtual void Upgrade()
    {
        levelsModels[currentLevel].SetActive(false);
        currentLevel++;
        levelsModels[currentLevel].SetActive(true);
    }

    public BaseTowerSO GetData()
    {
        return levelsData[currentLevel];
    }
}

public readonly struct TowerContext
{
    public readonly GameObject towerGO;
    public readonly BaseTower towerScript;
    public readonly TowerDetectionRange detectionRange;

    public TowerContext(GameObject towerGO, BaseTower towerScript, TowerDetectionRange detectionRange)
    {
        this.towerGO = towerGO;
        this.towerScript = towerScript;
        this.detectionRange = detectionRange;
    }
}
