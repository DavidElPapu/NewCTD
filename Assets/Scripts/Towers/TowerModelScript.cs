using System.Collections.Generic;
using UnityEngine;

public class TowerModelScript : MonoBehaviour
{
    [SerializeField] private MeshFilter[] modelMeshFilters;
    [SerializeField] private MeshRenderer[] modelMeshRenderers;

    public void SetMeshAndMaterials(IReadOnlyList<Mesh> newMeshes, IReadOnlyList<Material> newMaterials)
    {
        //We assume all the model parts have a meshFilter and a meshRenderer
        for (int i = 0; i < modelMeshFilters.Length; i++)
        {
            modelMeshFilters[i].mesh = newMeshes[i];
            modelMeshRenderers[i].material = newMaterials[i];
        }
    }
}
