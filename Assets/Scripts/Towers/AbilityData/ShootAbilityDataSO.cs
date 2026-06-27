using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShootAbilityData", menuName = "Towers/Abilities/ShootData")]
public class ShootAbilityDataSO : TowerAbilityDataSO
{
    public float damage;
    public float fireRate;
    public override Type GetAbilityComponentType() => typeof(ProjectileShooter);
}
