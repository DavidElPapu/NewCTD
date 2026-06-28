using UnityEngine;

public class MainTowerController : MonoBehaviour
{
    [SerializeField] private TowerModelScript[] levelModelScripts;
    [SerializeField] private TowerLevelsConfigSO levelsConfig;
    private ITowerAbility[] abilityComponents;
    private int currentLevel = 0, currentModelIndex = 0;

    private void Awake()
    {
        abilityComponents = GetComponents<ITowerAbility>();
        SetModelData();
        SetLevelData();
    }

    public bool CanUpgrade()
    {
        if (currentLevel < levelsConfig.LevelsData.Count - 1) return true;
        return false;
    }

    public void UpgradeTower()
    {
        currentLevel++;
        SetModelData();
        SetLevelData();
    }

    private void SetModelData()
    {
        TowerLevelData levelData = levelsConfig.LevelsData[currentLevel];

        //Shows tower model before chaging the mesh and textures
        int modelIndex = levelData.ModelIndex;
        if (currentModelIndex != modelIndex)
        {
            levelModelScripts[currentModelIndex].gameObject.SetActive(false);
            currentModelIndex = modelIndex;
            levelModelScripts[currentModelIndex].gameObject.SetActive(true);
        }

        //Passes corresponding readonlyList of Meshes and Materials to the model script
        levelModelScripts[currentModelIndex].SetMeshAndMaterials(levelData.ModelMeshes, levelData.ModelMaterials);
    }

    private void SetLevelData()
    {
        //Stop all ability components
        for (int i = 0; i < abilityComponents.Length; i++)
        {
            abilityComponents[i].Deactivate();
        }

        //Get all abilities of the current level
        TowerLevelData levelData = levelsConfig.LevelsData[currentLevel];
        for (int i = 0; i < levelData.Abilities.Length; i++)
        {
            //Get component from the ability data
            TowerAbilityDataSO abilityData = levelData.Abilities[i];
            System.Type componentType = abilityData.GetAbilityComponentType();

            //Looks for the component and initialize it
            for (int j = 0; j < abilityComponents.Length; j++)
            {
                if (abilityComponents[j].GetType() == componentType)
                {
                    abilityComponents[j].Initialize(abilityData, levelModelScripts[currentModelIndex]);
                    break;
                }
            }
        }
    }

    public void DeleteTower()
    {
        Destroy(gameObject);
    }

    public string GetName()
    {
        return levelsConfig.TName;
    }
}
