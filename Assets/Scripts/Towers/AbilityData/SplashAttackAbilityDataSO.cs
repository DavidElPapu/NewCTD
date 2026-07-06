using UnityEngine;

[CreateAssetMenu(fileName = "SplashAttackAbilityData", menuName = "Towers/AbilityData/SplashAttackData")]
public class SplashAttackAbilityDataSO : CooldownAttackAbilityDataSO
{
    public LayerMask splashDetectionLayers;

    public override System.Type GetAbilityComponentType() => typeof(SplashAttacker);
}
