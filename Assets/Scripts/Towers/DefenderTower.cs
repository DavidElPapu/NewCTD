using UnityEngine;

public class DefenderTower : BaseTower, IDamageable
{
    private HealthComponent healthComponent;

    protected override void Awake()
    {
        base.Awake();
        TryGetComponent(out healthComponent);
        healthComponent.SetMaxHealth(levelsData[currentLevel].maxHealth, true);
    }

    public override void Upgrade()
    {
        base.Upgrade();
        healthComponent.SetMaxHealth(levelsData[currentLevel].maxHealth, true);
    }

    public void TakeDamage(float damage)
    {
        healthComponent.DecreaseHealth(damage);
        if (healthComponent.CurrentHealth < 0)
            DeleteTower();
    }
}
