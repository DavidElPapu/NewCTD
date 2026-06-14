using UnityEngine;

[CreateAssetMenu(fileName = "NewAbility", menuName = "Tower Ability / DirectAttackAbility")]
public class DirectAttackAbility : TowerAbilitySO
{
    public override void TriggerAbility(in TowerContext context)
    {
        //I made this fast, this might need optimizing
        BaseEnemy targetEnemy = context.detectionRange.GetFirstEnemy();
        if (targetEnemy != null)
        {
            targetEnemy.TakeDamage(context.towerScript.GetData().damage);
            context.towerScript.GetModel().transform.GetChild(0).LookAt(targetEnemy.transform.position);
        }
    }
}
