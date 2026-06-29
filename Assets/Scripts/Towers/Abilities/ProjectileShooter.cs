using UnityEngine;

public class ProjectileShooter : MonoBehaviour, ITowerAbility
{
    [SerializeField] private TowerDetectionRange detectionRange;
    private ProjectileShootAbilityDataSO data;
    private ShooterModelScript modelScript;
    private Transform projectileSpawnPoint;
    private float cooldownTimer;

    public void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript)
    {
        //Set new data, restart cooldowns and also sets data for detectionRange
        this.data = (ProjectileShootAbilityDataSO)data;
        this.modelScript = (ShooterModelScript)modelScript;
        detectionRange.Initialize(this.data.detectionRange);
        projectileSpawnPoint = this.modelScript.projectileSpawnpoint;
        cooldownTimer = 0;
        enabled = true;
    }

    public void Deactivate()
    {
        //Disables the component
        if (enabled)
        {
            detectionRange.Deactivate();
            enabled = false;
        }
    }

    private void Update()
    {
        //Checks cooldown
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            return;
        }
        else
        {
            //If has enemy target, damages and resets cooldown
            BaseEnemy targetEnemy = detectionRange.GetFirstEnemy();
            if (targetEnemy != null)
            {
                modelScript.RotateToTarget(targetEnemy.gameObject.transform);
                TowerProjectileScript projectileScript = TowerProjectilePoolManager.singleton.GetBaseProjectileScript();
                projectileScript.Initialize(data.projectileMesh, data.projectileMaterial, data.detectionLayers, data.projectileSizeRadius, data.damage, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                projectileScript.LaunchProjectile(projectileSpawnPoint.forward * data.projectileSpeed);
                cooldownTimer += data.fireRate;
            }
        }
    }
}
