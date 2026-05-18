using UnityEngine;

public class ProcessContainerScript : ContainerScript
{
    [SerializeField] private Mesh defaultContentMesh, processedContentMesh;
    [SerializeField] private GameObject contentModel;
    private MeshFilter contentMeshFilter;
    private MeshRenderer contentMeshRenderer;
    private Color contentColor;
    private int currentProcessMeter, maxProcessMeter, ingredientProcessMeterValue;

    protected override void Awake()
    {
        base.Awake();
        contentModel.SetActive(false);
        contentModel.TryGetComponent(out contentMeshFilter);
        contentModel.TryGetComponent(out contentMeshRenderer);
        contentColor = Color.black;
        currentProcessMeter = 0;
        maxProcessMeter = 0;
        ingredientProcessMeterValue = 100;
    }

    public override void PlaceIngredient(IngredientScript ingredient)
    {
        base.PlaceIngredient(ingredient);
        maxProcessMeter += ingredientProcessMeterValue;
        if (!contentModel.activeSelf)
            contentModel.SetActive(true);
        if (contentMeshFilter.mesh != defaultContentMesh)
            contentMeshFilter.mesh = defaultContentMesh;
        if (contentColor == Color.black)
            contentColor = ingredient.data.baseColor;
        else
            contentColor = Color.Lerp(contentColor, ingredient.data.baseColor, 0.5f);
        contentMeshRenderer.material.color = contentColor;
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
        contentModel.SetActive(false);
        contentColor = Color.black;
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
