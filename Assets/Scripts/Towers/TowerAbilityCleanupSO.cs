using UnityEngine;

public abstract class TowerAbilityCleanupSO : ScriptableObject
{
    public abstract void Cleanup(in TowerContext context);
}
