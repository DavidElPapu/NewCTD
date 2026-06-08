using UnityEngine;

[CreateAssetMenu(fileName = "NewCleanup", menuName = "Tower Ability Cleanup / CooldownCleanup")]
public class CooldownCleanup : TowerAbilityCleanupSO
{
    public int priorityLevel;

    public override void Cleanup(in TowerContext context)
    {
        if (context.cooldownManager != null)
        {
            context.cooldownManager.SaveCooldownTime(priorityLevel);
        }
    }
}
