using UnityEngine;

public class IngredientScript : CookingObject
{
    public static int ingredientProcessMeter = 100;
    public IngredientSO data;
    public IngredientState state;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    public void ChangeMesh(Mesh newMesh)
    {
        meshFilter.sharedMesh = newMesh;
    }

    public void ChangeMaterial(Material newMaterial)
    {
        meshRenderer.sharedMaterial = newMaterial;
    }

    public override bool TryEnterItem(CookingObject item)
    {
        if (item is ContainerScript container)
        {
            if (container.CanPlaceIngredient(this))
            {
                container.PlaceIngredient(this);
            }
        }
        return false;
    }

    public override void OnPlayerInteraction(bool wasPicked)
    {
        //Nothing
    }
}
