using UnityEngine;

public abstract class TowerAbilityConditionSO : ScriptableObject
{
    public abstract void Setup(GameObject tower);
    public abstract bool IsValid();
}
