using UnityEngine;

[CreateAssetMenu(fileName = "HitscanShootAbilityData", menuName = "Towers/AbilityData/HitscanShootData")]
public class HitscanShootAbilityDataSO : CooldownAttackAbilityDataSO
{
    public override System.Type GetAbilityComponentType() => typeof(HitscanShooter);
}
