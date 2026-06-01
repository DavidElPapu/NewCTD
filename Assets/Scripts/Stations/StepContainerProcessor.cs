using UnityEngine;

public class StepContainerProcessor : MonoBehaviour, IHoldProcessor, IUseProcessor
{
    [SerializeField] private int processMeterValue;
    [Header("For processing without tool")]
    [SerializeField] private IngredientState processedState;
    private GameObject currentItem;
    private IngredientState currentProcessedState;

    private void Awake()
    {
        currentItem = null;
        currentProcessedState = IngredientState.Null;
    }

    public void OnItemEnter(GameObject item)
    {
        currentItem = item;
        if (processedState != IngredientState.Null)
            currentProcessedState = processedState;
    }

    public void OnItemExit(GameObject item)
    {
        currentItem = null;
        currentProcessedState = IngredientState.Null;
    }

    public void OnProcessorUse(GameObject usedItem)
    {
        if (currentItem == null) return;
        if (usedItem != null && usedItem.TryGetComponent(out ToolScript tool))
        {
            if (currentProcessedState == IngredientState.Null)
                currentProcessedState = tool.processedState;
            else if (tool.processedState != currentProcessedState)
                return;
        }
        if (currentItem.TryGetComponent(out ProcessContainerScript container) && !container.IsProcessDone())
        {
            container.AddProcessMeter(processMeterValue);
            if (container.IsProcessDone())
                container.OnProcessDone(currentProcessedState);
        }
    }
}
