using UnityEngine;

public class ProcessContainerScript : ContainerScript
{
    private int currentProcessMeter, maxProcessMeter, ingredientProcessMeterValue;

    protected override void Awake()
    {
        base.Awake();
        currentProcessMeter = 0;
        maxProcessMeter = 0;
        ingredientProcessMeterValue = 10;
    }

    public override void PlaceIngredient(IngredientScript ingredient)
    {
        base.PlaceIngredient(ingredient);
        maxProcessMeter += ingredientProcessMeterValue;
    }

    #region EmptyContainer

    public override bool CanEmptyContainer()
    {
        if (currentProcessMeter == 0 || currentProcessMeter == maxProcessMeter) return true;
        return false;
    }

    public override void EmptyContainer()
    {
        base.EmptyContainer();
        currentProcessMeter = 0;
        maxProcessMeter = 0;
    }

    #endregion

    #region Processing

    public void AddProcessMeter(int processValue)
    {
        currentProcessMeter += processValue;
    }

    public bool IsProcessDone()
    {
        if (currentProcessMeter >= maxProcessMeter)
        {
            currentProcessMeter = maxProcessMeter;
            return true;
        }
        return false;
    }

    public void OnProcessDone(IngredientState stateChange)
    {
        foreach (IngredientScript ingredient in containedIngredients)
        {
            ingredient.state = stateChange;
        }
    }

    #endregion
}
