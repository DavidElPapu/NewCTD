using UnityEngine;

public class ProjectileShooter : CooldownAttacker
{
    private ProjectileShootAbilityDataSO data;
    private ShooterModelScript modelScript;
    private Transform projectileSpawnPoint;

    public override void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript)
    {
        //Set new data, restart cooldowns and also sets data for detectionRange
        this.data = (ProjectileShootAbilityDataSO)data;
        this.modelScript = (ShooterModelScript)modelScript;
        detectionRange.Initialize(this.data.detectionRange, this.data.detectionLayers);
        projectileSpawnPoint = this.modelScript.projectileSpawnpoint;
        cooldownTimer = 0;
        currentCooldown = this.data.fireRate;
        enabled = true;
    }

    protected override void Attack(BaseEnemy enemy)
    {
        modelScript.RotateToTarget(enemy.gameObject.transform);
        TowerProjectileScript projectileScript = TowerProjectilePoolManager.singleton.GetBaseProjectileScript();
        projectileScript.Initialize(data, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
        projectileScript.LaunchProjectile(projectileSpawnPoint.forward * data.projectileSpeed);
    }
}
