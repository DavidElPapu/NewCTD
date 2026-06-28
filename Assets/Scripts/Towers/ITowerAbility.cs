using UnityEngine;

public interface ITowerAbility
{
    void Initialize(TowerAbilityDataSO data, TowerModelScript modelScript);
    void Deactivate();
}
