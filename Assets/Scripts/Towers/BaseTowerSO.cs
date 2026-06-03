using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Create Tower SO")]
public class BaseTowerSO : ScriptableObject
{
    public TowerName tName;
    public List<TowerAbility> abilities;
}

public enum TowerName
{
    Coconuturret,
    Pinacolada
}
