using UnityEngine;

public class MainTowerController : MonoBehaviour
{
    public string tName;
    [SerializeField] private GameObject[] levelsModels;
    [SerializeField] private TowerLevelData[] levelsData;
    private ITowerAbility[] abilityComponents;
    private int currentLevel = 0;

    private void Awake()
    {
        abilityComponents = GetComponents<ITowerAbility>();
        SetLevelData(currentLevel);
    }

    private void SetLevelData(int level)
    {
        //Stop all ability components
        for (int i = 0; i < abilityComponents.Length; i++)
        {
            abilityComponents[i].Deactivate();
        }

        //Get all abilities of the current level
        TowerLevelData levelData = levelsData[level];
        for (int i = 0; i < levelData.abilities.Length; i++)
        {
            //Get component from the ability data
            TowerAbilityDataSO abilityData = levelData.abilities[i];
            System.Type componentType = abilityData.GetAbilityComponentType();

            //Looks for the component and initialize it
            for (int j = 0; j < abilityComponents.Length; j++)
            {
                if (abilityComponents[j].GetType() == componentType)
                {
                    abilityComponents[j].Initialize(abilityData);
                    break;
                }
            }
        }
    }

    public bool CanUpgrade()
    {
        if (currentLevel < levelsData.Length - 1) return true;
        return false;
    }

    public void UpgradeTower()
    {
        //Shows upgraded model
        levelsModels[currentLevel].SetActive(false);
        currentLevel++;
        levelsModels[currentLevel].SetActive(true);

        //Sets upgraded data
        SetLevelData(currentLevel);
    }

    public virtual void DeleteTower()
    {
        Destroy(gameObject);
    }
}

[System.Serializable]
public struct TowerLevelData
{
    public TowerAbilityDataSO[] abilities;
}
