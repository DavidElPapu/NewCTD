using UnityEngine;

public class ProjectileShooter : MonoBehaviour, ITowerAbility
{
    private ShootAbilityDataSO stats;
    private Transform firePoint;
    private float timer;

    public void Initialize(TowerAbilityDataSO data)
    {
        stats = (ShootAbilityDataSO)data;
        enabled = true;
    }

    public void Deactivate()
    {
        //Disables the component
        if (enabled)
            enabled = false;
    }

    private void Update()
    {
        // Simple, flat polling timer. Zero GC, lightning fast.
        timer += Time.deltaTime;
        if (timer >= stats.fireRate)
        {
            timer = 0;
            Debug.Log("dISPARO");
            // Execute shoot logic using stats.damage...
        }
    }
}
