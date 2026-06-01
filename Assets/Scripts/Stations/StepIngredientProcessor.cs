using UnityEngine;

public class StepIngredientProcessor : MonoBehaviour, IHoldProcessor, IUseProcessor
{
    [SerializeField] private Mesh processedMesh;
    [SerializeField] private Vector3 customSize, customOffset;
    [SerializeField] private int processMeterValue;
    [Header("For processing without tool")]
    [SerializeField] private IngredientState processedState;
    private GameObject currentItem;
    private IngredientState currentProcessedState;
    private int currentProcessMeter;

    private void Awake()
    {
        currentItem = null;
        currentProcessedState = IngredientState.Null;
        currentProcessMeter = 0;
    }

    public void OnItemEnter(GameObject item)
    {
        currentItem = item;
        currentProcessMeter = 0;
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
        if (currentItem.TryGetComponent(out IngredientScript ingredient) && currentProcessMeter < IngredientScript.ingredientProcessMeter)
        {
            currentProcessMeter += processMeterValue;
            if (currentProcessMeter >= IngredientScript.ingredientProcessMeter)
            {
                ingredient.state = currentProcessedState;
                ingredient.ChangeMesh(processedMesh);
                ingredient.ChangeSizeAndOffset(customSize, customOffset);
            }
        }
    }
}
