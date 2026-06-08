using UnityEngine;
using System.Collections.Generic;

public class BaseTower : MonoBehaviour
{
    [SerializeField] protected List<BaseTowerSO> levelsData;
    [SerializeField] private List<GameObject> levelsModels;
    private List<ITowerComponent> towerComponents = new List<ITowerComponent>();
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
        towerComponents.AddRange(GetComponents<ITowerComponent>());
        SetTowerComponentsData();
        context = new TowerContext
        (
            gameObject,
            this,
            GetComponent<TowerDetectionRange>(),
            GetComponent<TowerCooldownManager>()
        );
    }

    private void Update()
    {
        foreach (TowerAbility towerAbility in GetData().abilities)
        {
            bool canTriggerAbility = true;
            foreach (TowerAbilityConditionSO condition in towerAbility.conditions)
            {
                if (!condition.IsValid(context))
                {
                    canTriggerAbility = false;
                    break;
                }
            }
            if (canTriggerAbility)
            {
                towerAbility.ability.TriggerAbility(context);
                foreach (TowerAbilityCleanupSO cleanup in towerAbility.cleanups)
                {
                    cleanup.Cleanup(context);
                }
            }
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
        SetTowerComponentsData();
    }

    public BaseTowerSO GetData()
    {
        return levelsData[currentLevel];
    }

    private void SetTowerComponentsData()
    {
        foreach (ITowerComponent towerComponent in towerComponents)
        {
            towerComponent.SetupData(GetData());
        }
    }
}

public readonly struct TowerContext
{
    public readonly GameObject towerGO;
    public readonly BaseTower towerScript;
    public readonly TowerDetectionRange detectionRange;
    public readonly TowerCooldownManager cooldownManager;

    public TowerContext(GameObject towerGO, BaseTower towerScript, TowerDetectionRange detectionRange, TowerCooldownManager cooldownManager)
    {
        this.towerGO = towerGO;
        this.towerScript = towerScript;
        this.detectionRange = detectionRange;
        this.cooldownManager = cooldownManager;
    }
}
