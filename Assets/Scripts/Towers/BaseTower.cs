using UnityEngine;
using System.Collections.Generic;

public abstract class BaseTower : MonoBehaviour
{
    //Reference to the SO
    //DetectionRange
    [SerializeField] private List<GameObject> levelModels;
    protected List<GameObject> enemiesInRange = new List<GameObject>();
    private int currentLevel = 0;

    protected virtual void Awake()
    {
        for (int i = 0; i < levelModels.Count; i++)
        {
            if (i != currentLevel)
                levelModels[i].SetActive(false);
            else
                levelModels[i].SetActive(true);
        }
    }

    public virtual bool CanUpgrade()
    {
        if (currentLevel < levelModels.Count - 1) return true;
        return false;
    }

    public virtual void OnUpgrade()
    {
        levelModels[currentLevel].SetActive(false);
        currentLevel++;
        levelModels[currentLevel].SetActive(true);
    }

    protected abstract void OnMainAction();

}
