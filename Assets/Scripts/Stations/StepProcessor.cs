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
    //stepMeterValue is relative to requiredSteps for ingredients and relative to a 100 for containers, this might change for consistency
    [SerializeField] private int processMeterValue;
    [Header("For processing without tool")]
    [SerializeField] private IngredientState processedState;
    private UsableStation usableStation;
    private ItemHolder itemHolder;
    private GameObject currentItem;
    private IngredientState currentProcessedState;

    private void Awake()
    {
        TryGetComponent(out usableStation);
        TryGetComponent(out itemHolder);
        currentItem = null;
        currentProcessMeter = 0;
        currentProcessedState = IngredientState.Null;
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
        if (processedState != IngredientState.Null)
            currentProcessedState = processedState;
    }

    private void OnItemLeave(GameObject item)
    {
        currentItem = null;
        currentProcessedState = IngredientState.Null;
    }

    private void OnUse(GameObject usedItem)
    {
        if (currentItem == null) return;
        if (usedItem != null && usedItem.TryGetComponent(out ToolScript tool))
        {
            if (currentProcessedState == IngredientState.Null)
                currentProcessedState = tool.processedState;
            else if (tool.processedState != currentProcessedState)
                return;
        }
        if (currentItem.TryGetComponent(out IngredientScript ingredient) && currentProcessMeter < maxProcessMeter)
        {
            currentProcessMeter += processMeterValue;
            if (currentProcessMeter >= maxProcessMeter)
            {
                ingredient.state = currentProcessedState;
                ingredient.ChangeMesh(processedMesh);
                ingredient.ChangeSizeAndOffset(customSize, customOffset);
            }
        }
        else if (currentItem.TryGetComponent(out ProcessContainerScript container) && !container.IsProcessDone())
        {
            container.AddProcessMeter(processMeterValue);
            if (container.IsProcessDone())
                container.OnProcessDone(currentProcessedState);
        }
    }
}
