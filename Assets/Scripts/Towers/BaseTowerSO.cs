using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Create Tower SO")]
public class BaseTowerSO : ScriptableObject
{
    public TowerName tName;
    [Header("Health Data")]
    public float maxHealth;
    public int damage;
    public float range1;
    public float cooldown1;
    public float cooldown2;
    public float cooldown3;
    public List<TowerAbility> abilities;
}

public enum TowerName
{
    Coconuturret,
    Pinacolada
}
