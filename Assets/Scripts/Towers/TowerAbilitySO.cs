using UnityEngine;

public abstract class TowerAbilitySO : ScriptableObject
{
    public abstract void Setup(GameObject tower);
    public abstract void TriggerAbility();
}
