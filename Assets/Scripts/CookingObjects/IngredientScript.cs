using UnityEngine;

public class IngredientScript : MonoBehaviour, ICookingObject
{
    public static int ingredientProcessMeter = 100;
    public IngredientSO data;
    public IngredientState state;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    public CookingObjectName GetCookingObjectName()
    {
        return data.iName;
    }

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
}
