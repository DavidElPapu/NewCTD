using UnityEngine;

public class TowerCooldownManager : MonoBehaviour, ITowerComponent
{
    private float[] cooldownTimeSavers = new float[5];

    public void SetupData(BaseTowerSO newData)
    {
        System.Array.Clear(cooldownTimeSavers, 0, cooldownTimeSavers.Length);
    }

    public bool IsCooldownReady(int cooldownKey, float cooldownTime)
    {
        return Time.time >= cooldownTimeSavers[cooldownKey] + cooldownTime;
    }

    public void SaveCooldownTime(int cooldownKey)
    {
        cooldownTimeSavers[cooldownKey] = Time.time;
    }
}
