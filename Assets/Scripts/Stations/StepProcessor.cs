using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepProcessor : ItemHolderStation
{
    [SerializeField] private List<CookingObjectName> validTools;
    [SerializeField] private float useCooldown;
    private ProcessorTool currentTool;
    private bool canBeUsed;

    [Header("For processing Ingredients")]
    private int currentProcessMeter;

    [Header("For processing without tool")]
    [SerializeField] private ProcessorTool internalTool;

    protected override void Awake()
    {
        base.Awake();
        canBeUsed = true;
        currentTool = internalTool;
        currentProcessMeter = 0;
    }

    public override CookingObject TryGetItem()
    {
        //If an ingredient is being processed, it can't be picked
        if (currentProcessMeter > 0 && currentProcessMeter < IngredientScript.ingredientProcessMeter) return null;
        return base.TryGetItem();
    }

    protected override void UpdateItemChange()
    {
        if (itemHeld == null || (itemHeld is ProcessContainerScript container && container.IsProcessDone()))
        {
            //This resets the tool for both cases, if is using an internal tool it stays the same, if uses external means the internal variable is null, so it gets reseted
            currentTool = internalTool;
            currentProcessMeter = 0;
        }
    }

    public override void UseStation(CookingObject item)
    {
        if (itemHeld == null || !canBeUsed || !IsItemValid(item)) return;

        //If this station has a default process state or a different to the tool, it ignores the tool state and exits
        if (item != null && item is ProcessorTool tool)
        {
            if (currentTool != null && tool.cName != currentTool.cName) return;
            currentTool = tool;
        }

        //Then checks if its holding an ingredient or container, to process the ingredient directly or the content of the container
        if (itemHeld is IngredientScript ingredient && currentProcessMeter < IngredientScript.ingredientProcessMeter)
        {
            currentProcessMeter += currentTool.processMeterValue;
            if (currentProcessMeter >= IngredientScript.ingredientProcessMeter)
            {
                ingredient.state = currentTool.processedState;
                ingredient.ChangeMesh(currentTool.processedMesh);
            }
        }
        else if (itemHeld is ProcessContainerScript container && !container.IsProcessDone())
        {
            container.AddProcessMeter(currentTool.processMeterValue);
            if (container.IsProcessDone())
            {
                container.OnProcessDone(currentTool.processedState);
            }
        }
        canBeUsed = false;
        StartCoroutine(Cooldown());
    }

    private bool IsItemValid(CookingObject item)
    {
        if (validTools.Count == 0) return true;
        if (item != null)
        {
            foreach (CookingObjectName tool in validTools)
            {
                if (item.cName == tool) return true;
            }
        }
        return false;
    }

    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(useCooldown);
        canBeUsed = true;
    }
}
