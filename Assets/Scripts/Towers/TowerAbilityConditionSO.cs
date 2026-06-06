using UnityEngine;

public abstract class TowerAbilityConditionSO : ScriptableObject
{
    public abstract bool IsValid(in TowerContext context);
}
