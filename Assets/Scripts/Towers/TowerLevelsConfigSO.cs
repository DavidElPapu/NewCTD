using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerLevelsConfig", menuName = "Towers/TowerLevelsConfig")]
public class TowerLevelsConfigSO : ScriptableObject
{
    [SerializeField] private string tName;
    [SerializeField] private TowerLevelData[] levelsData;

    //Get only properties
    public string TName => tName;
    public IReadOnlyList<TowerLevelData> LevelsData => levelsData;
}

[Serializable]
public struct TowerLevelData
{
    [SerializeField] private TowerAbilityDataSO[] abilities;
    [SerializeField] private int modelIndex;
    [SerializeField] private Mesh[] modelMeshes;
    [SerializeField] private Material[] modelMaterials;
    //Get only properties
    public readonly ReadOnlySpan<TowerAbilityDataSO> Abilities => abilities;
    public int ModelIndex => modelIndex;
    public readonly IReadOnlyList<Mesh> ModelMeshes => modelMeshes;
    public readonly IReadOnlyList<Material> ModelMaterials => modelMaterials;
}
