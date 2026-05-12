using UnityEngine;

public class IngredientScript : MonoBehaviour, ICookingObject
{
    public IngredientSO data;
    [SerializeField] private MeshFilter meshFilter;
    [SerializeField] private MeshRenderer meshRenderer;

    public CookingObjectName GetCookingObjectName()
    {
        return data.iName;
    }

    public CookingObjectType GetCookingObjectType()
    {
        return CookingObjectType.Ingredient;
    }

    public void ChangeMesh(Mesh newMesh)
    {
        meshFilter.mesh = newMesh;
    }

    public void ChangeMaterial(Material newMaterial)
    {
        meshRenderer.material = newMaterial;
    }

    public void ChangeSize(Vector3 newSize)
    {
        transform.localScale = newSize;
    }
}
