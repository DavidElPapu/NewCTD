using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Tower Ability / DirectAttackAbility")]
public class DirectAttackAbility : TowerAbilitySO
{
    public override void TriggerAbility(in TowerContext context)
    {
        //I made this fast, this might need optimizing
        GameObject targetEnemy = context.detectionRange.GetFirstEnemy();
        if (targetEnemy.TryGetComponent(out IEnemyHealth enemyHealth))
        {
            enemyHealth.DealDamage(context.towerScript.GetData().damage);
            context.towerGO.transform.LookAt(targetEnemy.transform.position);
        }
    }
}
