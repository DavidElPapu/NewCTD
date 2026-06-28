using UnityEngine;

public class HitscanShooter : MonoBehaviour, ITowerAbility
{
    [SerializeField] private TowerDetectionRange detectionRange;
    private HitscanShootAbilityDataSO data;
    private ShooterModelScript modelScript;
    private float timer;

    public void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript)
    {
        //Set new data, restart cooldowns and also sets data for detectionRange
        this.data = (HitscanShootAbilityDataSO)data;
        this.modelScript = (ShooterModelScript)modelScript;
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
        }

        //Checks for detection Range
        if (detectionRange.enemiesInRange.Count > 0)
        {
            BaseEnemy targetEnemy = detectionRange.GetFirstEnemy();
            modelScript.RotateToTarget(targetEnemy.gameObject.transform);
            if (timer <= 0)
            {
                targetEnemy.TakeDamage(data.damage);
                timer += data.fireRate;
            }
        }
    }
}
