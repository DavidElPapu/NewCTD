using UnityEngine;

public abstract class TowerAbilityConditionSO : ScriptableObject
{
    public abstract bool IsValid(GameObject tower);
}
