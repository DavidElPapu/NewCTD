using UnityEngine;

public class HitscanShooter : MonoBehaviour, ITowerAbility
{
    [SerializeField] private TowerDetectionRange detectionRange;
    private HitscanShootAbilityDataSO data;
    private ShooterModelScript modelScript;
    private float cooldownTimer;

    public void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript)
    {
        //Set new data, restart cooldowns and also sets data for detectionRange
        this.data = (HitscanShootAbilityDataSO)data;
        this.modelScript = (ShooterModelScript)modelScript;
        detectionRange.Initialize(this.data.detectionRange, this.data.detectionLayers);
        cooldownTimer = 0;
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
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
        }
        else
        {
            //If has enemy target, damages and resets cooldown
            BaseEnemy targetEnemy = detectionRange.GetFirstEnemy();
            if (targetEnemy != null)
            {
                modelScript.RotateToTarget(targetEnemy.gameObject.transform);
                targetEnemy.TakeDamage(data.damage);
                cooldownTimer += data.fireRate;
            }
        }
    }
}
