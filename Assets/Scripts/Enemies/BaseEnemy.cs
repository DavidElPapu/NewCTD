using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class BaseEnemy : MonoBehaviour, IDamageable
{
    public event Action<BaseEnemy> OnEnemyDeath;
    private HealthComponent healthComponent;

    private void Awake()
    {
        TryGetComponent(out healthComponent);
        healthComponent.SetMaxHealth(100f, true);
    }

    private void OnDeath()
    {
        OnEnemyDeath?.Invoke(this);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        healthComponent.ModifyHealth(-damage);
        if (healthComponent.CurrentHealth <= 0)
            OnDeath();
    }

    public float GetDistanceToBase()
    {
        //this should return an already calculated distance value, maybe done in a custom coroutine every 0.05 seconds or more
        return 10f;
    }
}
