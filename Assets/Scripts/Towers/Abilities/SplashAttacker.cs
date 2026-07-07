using UnityEngine;

public class SplashAttacker : CooldownAttacker
{
    private SplashAttackAbilityDataSO data;
    //This isn't used now, but might be used later for animations
    private TowerModelScript modelScript;
    //To avoid garbage collection, we declare a fixed size array for colliders that OverlapSphereNonAlloc will use
    //For now, 50 is the max number of enemies it can detect
    private Collider[] enemyColliders = new Collider[50];

    public override void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript)
    {
        //Set new data, restart cooldowns and also sets data for detectionRange
        this.data = (SplashAttackAbilityDataSO)data;
        this.modelScript = modelScript;
        detectionRange.Initialize(this.data.detectionRange, this.data.detectionLayers);
        cooldownTimer = 0;
        currentCooldown = this.data.fireRate;
        enabled = true;
    }

    protected override void Attack(BaseEnemy enemy)
    {
        int enemyCount = Physics.OverlapSphereNonAlloc(transform.position, data.detectionRange, enemyColliders, data.splashDetectionLayers, QueryTriggerInteraction.Collide);

        //Loops through enemies found and deals damage and apply status effect to them
        for (int i = 0; i < enemyCount; i++)
        {
            if (enemyColliders[i].gameObject.TryGetComponent(out BaseEnemy enemy2))
            {
                enemy2.TakeDamage(data.damage);
                enemy2.ApplyStatusEffect(data.applyEffect, data.effectDuration);
            }

            //Cleans the element from the array to avoid using old data in future casts
            enemyColliders[i] = null;
        }
    }
}
