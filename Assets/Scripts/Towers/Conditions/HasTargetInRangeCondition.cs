using UnityEngine;

[CreateAssetMenu(fileName = "NewCondition", menuName = "Tower Ability Condition / HasTargetCondition")]
public class HasTargetInRangeCondition : TowerAbilityConditionSO
{
    public override bool IsValid(in TowerContext context)
    {
        if (context.detectionRange == null || context.detectionRange.enemiesInRange.Count <= 0) return false;
        return true;
    }
}
