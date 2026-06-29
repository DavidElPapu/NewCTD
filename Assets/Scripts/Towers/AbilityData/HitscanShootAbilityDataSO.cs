using UnityEngine;

[CreateAssetMenu(fileName = "HitscanShootAbilityData", menuName = "Towers/AbilityData/HitscanShootData")]
public class HitscanShootAbilityDataSO : TowerAbilityDataSO
{
    [Header("TowerStats")]
    public float damage;
    public float fireRate;
    [Header("Detection")]
    public LayerMask detectionLayers;
    public float detectionRange;
    public override System.Type GetAbilityComponentType() => typeof(HitscanShooter);
}
