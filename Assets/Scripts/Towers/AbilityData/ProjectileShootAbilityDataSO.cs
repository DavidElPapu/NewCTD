using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileShootAbilityData", menuName = "Towers/AbilityData/ProjectileShootData")]
public class ProjectileShootAbilityDataSO : TowerAbilityDataSO
{
    public GameObject projectilePrefab;
    public float damage;
    public float fireRate;
    public float range;
    public override System.Type GetAbilityComponentType() => typeof(HitscanShooter);
}
