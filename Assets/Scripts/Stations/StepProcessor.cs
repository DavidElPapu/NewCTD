using UnityEngine;

[RequireComponent(typeof(UsableStation), typeof(ItemHolder))]
public class StepProcessor : MonoBehaviour
{
    [Header("For Ingredient Processing")]
    [SerializeField] private Mesh processedMesh;
    [SerializeField] private Vector3 customSize, customOffset;
    [SerializeField] private int maxProcessMeter;
    private int currentProcessMeter;
    [Header("For Ingredient or Container Processing")]
    [SerializeField] private IngredientState processedState;
    //stepMeterValue is relative to requiredSteps for ingredients and relative to a 100 for containers, this might change for consistency
    [SerializeField] private int processMeterValue;
    private UsableStation usableStation;
    private ItemHolder itemHolder;
    private GameObject currentItem;

    private void Awake()
    {
        TryGetComponent(out usableStation);
        TryGetComponent(out itemHolder);
        currentItem = null;
        currentProcessMeter = 0;
    }

    private void OnEnable()
    {
        usableStation.OnStationUse += OnUse;
        itemHolder.OnItemHold += OnItemEnter;
        itemHolder.OnItemLeave += OnItemLeave;
    }

    private void OnDisable()
    {
        usableStation.OnStationUse -= OnUse;
        itemHolder.OnItemHold -= OnItemEnter;
        itemHolder.OnItemLeave -= OnItemLeave;
    }

    private void OnItemEnter(GameObject item)
    {
        currentItem = item;
        currentProcessMeter = 0;
    }

    private void OnItemLeave(GameObject item)
    {
        currentItem = null;
    }

    private void OnUse(GameObject usedItem)
    {
        if (currentItem == null) return;
        if (currentItem.TryGetComponent(out IngredientScript ingredient) && currentProcessMeter < maxProcessMeter)
        {
            currentProcessMeter += processMeterValue;
            if (currentProcessMeter >= maxProcessMeter)
            {
                ingredient.state = processedState;
                ingredient.ChangeMesh(processedMesh);
                ingredient.ChangeSizeAndOffset(customSize, customOffset);
            }
        }
        else if (currentItem.TryGetComponent(out ProcessContainerScript container) && !container.IsProcessDone())
        {
            container.AddProcessMeter(processMeterValue);
            if (container.IsProcessDone())
                container.OnProcessDone(processedState);
        }
    }
}
