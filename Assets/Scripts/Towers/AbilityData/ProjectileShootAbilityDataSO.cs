using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileShootAbilityData", menuName = "Towers/AbilityData/ProjectileShootData")]
public class ProjectileShootAbilityDataSO : CooldownAttackAbilityDataSO
{
    [Header("Projectile")]
    public TowerProjectileType projectileType;
    public Mesh projectileMesh;
    public Material projectileMaterial;
    public float projectileSizeRadius;
    public float projectileSpeed;
    public float projectileActiveTime;
    public int projectileMaxImpacts;
    public override System.Type GetAbilityComponentType() => typeof(ProjectileShooter);
}
