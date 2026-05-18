using UnityEngine;

public class IngredientScript : MonoBehaviour, ICookingObject
{
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
        meshFilter.mesh = newMesh;
    }

    public void ChangeMaterial(Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }

    public void ChangeSizeAndOffset(Vector3 newSize, Vector3 newOffset)
    {
        meshRenderer.transform.localScale = newSize;
        meshRenderer.transform.localPosition = newOffset;
    }
}
