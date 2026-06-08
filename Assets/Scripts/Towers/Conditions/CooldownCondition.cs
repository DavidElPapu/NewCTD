using UnityEngine;

[CreateAssetMenu(fileName = "NewCondition", menuName = "Tower Ability Condition / CooldownCondition")]
public class CooldownCondition : TowerAbilityConditionSO
{
    public int priorityLevel;

    public override bool IsValid(in TowerContext context)
    {
        if (context.cooldownManager != null && context.towerScript != null)
        {
            float cooldownTime;
            switch (priorityLevel)
            {
                case 1:
                    cooldownTime = context.towerScript.GetData().cooldown1;
                    break;
                case 2:
                    cooldownTime = context.towerScript.GetData().cooldown2;
                    break;
                case 3:
                    cooldownTime = context.towerScript.GetData().cooldown3;
                    break;
                default:
                    cooldownTime = 0f;
                    break;
            }
            return context.cooldownManager.IsCooldownReady(priorityLevel - 1, cooldownTime);
        }
        return false;
    }
}
