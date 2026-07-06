using UnityEngine;

public class ExplosiveProjectileScript : TowerProjectileScript
{
    //To avoid garbage collection, we declare a fixed size array for colliders that OverlapSphereNonAlloc will use
    //For now, 10 is the max number of enemies it can detect
    private Collider[] enemyColliders = new Collider[10];
    private ExplosiveProjectileShootAbilityDataSO EData => data as ExplosiveProjectileShootAbilityDataSO;

    protected override void OnImpact(BaseEnemy enemy)
    {
        int enemyCount = Physics.OverlapSphereNonAlloc(transform.position, EData.explosionRadius, enemyColliders, EData.explosionDetectionLayers, QueryTriggerInteraction.Collide);
        
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
