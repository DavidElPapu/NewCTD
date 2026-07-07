using UnityEngine;

[CreateAssetMenu(fileName = "ExplosiveProjectileShootAbilityData", menuName = "Towers/AbilityData/ExplosiveProjectileShootData")]
public class ExplosiveProjectileShootAbilityDataSO : ProjectileShootAbilityDataSO
{
    public float explosionRadius;
    public LayerMask explosionDetectionLayers;
}
