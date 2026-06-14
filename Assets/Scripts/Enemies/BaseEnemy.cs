using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour, IDamageable
{
    public event Action<BaseEnemy> OnEnemyDeath;
    private HealthComponent healthComponent;

    private void OnDeath()
    {
        OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        healthComponent.DecreaseHealth(damage);
        if (healthComponent.CurrentHealth < 0)
            OnDeath();
    }

    public float GetDistanceToBase()
    {
        //this should return an already calculated distance value, maybe done in a custom coroutine every 0.05 seconds or more
        return 10f;
    }
}
