using UnityEngine;
using System.Collections.Generic;

public class BaseTower : MonoBehaviour
{
    [SerializeField] protected List<BaseTowerSO> levelsData;
    [SerializeField] private List<GameObject> levelsModels;
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

    public TowerName GetName()
    {
        return levelsData[currentLevel].tName;
    }
}
