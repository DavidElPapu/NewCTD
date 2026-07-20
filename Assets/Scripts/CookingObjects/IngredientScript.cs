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

    public void ChangeSizeAndOffset(Vector3 newSize, Vector3 newOffset)
    {
        meshRenderer.transform.localScale = newSize;
        meshRenderer.transform.localPosition = newOffset;
    }

    public override bool TryEnterItem(CookingObject item)
    {
        //This method only return true if the item can enter this cookingObject, since this is an ingredient, it can only return false but still enter a container
        if (item is ContainerScript container)
        {
            if (container.CanPlaceIngredient(this))
            {
                container.PlaceIngredient(this);
            }
        }
        return false;
    }
}
