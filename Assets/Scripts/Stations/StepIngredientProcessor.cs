using UnityEngine;

public class StepIngredientProcessor : MonoBehaviour, IHoldProcessor, IUseProcessor
{
    [SerializeField] private Mesh processedMesh;
    [SerializeField] private Vector3 customSize, customOffset;
    [SerializeField] private int processMeterValue;
    [Header("For processing without tool")]
    [SerializeField] private IngredientState processedState;
    private CookingObject currentItem;
    private IngredientState currentProcessedState;
    private int currentProcessMeter;

    private void Awake()
    {
        currentItem = null;
        currentProcessedState = IngredientState.Null;
        currentProcessMeter = 0;
    }

    public void OnItemEnter(CookingObject item)
    {
        currentItem = item;
        currentProcessMeter = 0;
        if (processedState != IngredientState.Null)
            currentProcessedState = processedState;
    }

    public void OnItemExit(CookingObject item)
    {
        currentItem = null;
        currentProcessedState = IngredientState.Null;
    }

    public void OnProcessorUse(CookingObject usedItem)
    {
        if (currentItem == null) return;
        if (usedItem != null && usedItem is ToolScript tool)
        {
            if (currentProcessedState == IngredientState.Null)
                currentProcessedState = tool.processedState;
            else if (tool.processedState != currentProcessedState)
                return;
        }
        if (currentItem is IngredientScript ingredient && currentProcessMeter < IngredientScript.ingredientProcessMeter)
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
