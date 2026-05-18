using UnityEngine;

public class ProcessContainerScript : ContainerScript
{
    [SerializeField] private Mesh defaultContentMesh, processedContentMesh;
    private MeshFilter contentMeshFilter;
    private int currentProcessMeter, maxProcessMeter, ingredientProcessMeterValue;

    protected override void Awake()
    {
        base.Awake();
        contentModel.TryGetComponent(out contentMeshFilter);
        currentProcessMeter = 0;
        maxProcessMeter = 0;
        ingredientProcessMeterValue = 100;
    }

    public override void PlaceIngredient(IngredientScript ingredient)
    {
        base.PlaceIngredient(ingredient);
        maxProcessMeter += ingredientProcessMeterValue;
        if (contentMeshFilter.mesh != defaultContentMesh)
            contentMeshFilter.mesh = defaultContentMesh;
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
        contentMeshFilter.mesh = processedContentMesh;
    }

    #endregion
}
