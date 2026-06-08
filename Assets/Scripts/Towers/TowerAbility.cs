using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TowerAbility
{
    public TowerAbilitySO ability;
    public List<TowerAbilityConditionSO> conditions;
    public List<TowerAbilityCleanupSO> cleanups;
}
