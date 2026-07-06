using UnityEngine;

public class HitscanShooter : CooldownAttacker
{
    private HitscanShootAbilityDataSO data;
    private ShooterModelScript modelScript;

    public override void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript)
    {
        //Set new data, restart cooldowns and also sets data for detectionRange
        this.data = (HitscanShootAbilityDataSO)data;
        this.modelScript = (ShooterModelScript)modelScript;
        detectionRange.Initialize(this.data.detectionRange, this.data.detectionLayers);
        cooldownTimer = 0;
        currentCooldown = this.data.fireRate;
        enabled = true;
    }

    protected override void Attack(BaseEnemy enemy)
    {
        modelScript.RotateToTarget(enemy.gameObject.transform);
        enemy.TakeDamage(data.damage);
        enemy.ApplyStatusEffect(data.applyEffect, data.effectDuration);
    }
}
