using UnityEngine;

public class ProjectileShooter : MonoBehaviour, ITowerAbility
{
    [SerializeField] private TowerDetectionRange detectionRange;
    private ProjectileShootAbilityDataSO data;
    private ShooterModelScript modelScript;
    private Transform projectileSpawnPoint;
    private float timer;

    public void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript)
    {
        //Set new data, restart cooldowns and also sets data for detectionRange
        this.data = (ProjectileShootAbilityDataSO)data;
        this.modelScript = (ShooterModelScript)modelScript;
        detectionRange.Initialize(this.data.range);
        projectileSpawnPoint = this.modelScript.projectileSpawnpoint;
        timer = 0;
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
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }

        //Checks for detection Range
        if (detectionRange.enemiesInRange.Count > 0)
        {
            Shoot();
            timer += data.fireRate;
        }
    }

    private void Shoot()
    {
        //This needs object pooling
        GameObject newProjectile = Instantiate(data.projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
    }
}
