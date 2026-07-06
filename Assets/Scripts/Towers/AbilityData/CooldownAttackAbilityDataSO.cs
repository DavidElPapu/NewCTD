using UnityEngine;

public abstract class CooldownAttackAbilityDataSO : TowerAbilityDataSO
{
    [Header("TowerStats")]
    public float damage;
    public float fireRate;
    public StatusEffect applyEffect;
    public float effectDuration;
    [Header("Detection")]
    public LayerMask detectionLayers;
    public float detectionRange;
    public override System.Type GetAbilityComponentType() => typeof(CooldownAttacker);
}
