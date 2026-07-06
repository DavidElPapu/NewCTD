using UnityEngine;

public abstract class CooldownAttacker : MonoBehaviour, ITowerAbility
{
    [SerializeField] protected TowerDetectionRange detectionRange;
    protected float cooldownTimer, currentCooldown;

    public abstract void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript);

    public void Deactivate()
    {
        //Disables the component and the detectionRange
        if (enabled)
        {
            detectionRange.Deactivate();
            enabled = false;
        }
    }

    protected void Update()
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
                Attack(targetEnemy);
                cooldownTimer += currentCooldown;
            }
        }
    }

    protected abstract void Attack(BaseEnemy enemy);
}
