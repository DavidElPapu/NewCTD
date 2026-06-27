using UnityEngine;

public class HitscanShooter : MonoBehaviour, ITowerAbility
{
    [SerializeField] TowerDetectionRange detectionRange;
    private HitscanShootAbilityDataSO data;
    private float timer;

    public void Initialize(TowerAbilityDataSO data)
    {
        //Set new data, restart cooldowns and also sets data for detectionRange
        this.data = (HitscanShootAbilityDataSO)data;
        detectionRange.Initialize(this.data.range);
        timer = 0;
        enabled = true;
    }

    public void Deactivate()
    {
        //Disables the component and the detectionRange
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
        BaseEnemy targetEnemy = detectionRange.GetFirstEnemy();
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(data.damage);
        }
    }
}
