using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileShootAbilityData", menuName = "Towers/AbilityData/ProjectileShootData")]
public class ProjectileShootAbilityDataSO : TowerAbilityDataSO
{
    [Header("TowerStats")]
    public float damage;
    public float fireRate;
    [Header("Detection")]
    public LayerMask detectionLayers;
    public float detectionRange;
    [Header("Projectile")]
    public GameObject projectilePrefab;
    public Mesh projectileMesh;
    public Material projectileMaterial;
    public float projectileSizeRadius;
    public float projectileSpeed;
    public override System.Type GetAbilityComponentType() => typeof(ProjectileShooter);
}
