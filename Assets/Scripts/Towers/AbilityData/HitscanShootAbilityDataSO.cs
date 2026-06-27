using UnityEngine;

[CreateAssetMenu(fileName = "HitscanShootAbilityData", menuName = "Towers/AbilityData/HitscanShootData")]
public class HitscanShootAbilityDataSO : TowerAbilityDataSO
{
    public float damage;
    public float fireRate;
    public float range;
    public override System.Type GetAbilityComponentType() => typeof(HitscanShooter);
}
