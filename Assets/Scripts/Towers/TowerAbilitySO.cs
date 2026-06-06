using UnityEngine;

public abstract class TowerAbilitySO : ScriptableObject
{
    public abstract void TriggerAbility(in TowerContext context);
}
