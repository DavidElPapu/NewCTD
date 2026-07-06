using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class BaseEnemy : MonoBehaviour, IDamageable
{
    public event Action<BaseEnemy> OnEnemyDeath;
    private float[] statusEffectsRemainingTimes;
    private HealthComponent healthComponent;

    private void Awake()
    {
        statusEffectsRemainingTimes = new float[3];
        TryGetComponent(out healthComponent);
        healthComponent.SetMaxHealth(100f, true);
    }

    private void Update()
    {
        UpdateStatusEffects();
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

    public void ApplyStatusEffect(StatusEffect effect, float duration)
    {
        if (duration > statusEffectsRemainingTimes[(int)effect])
            statusEffectsRemainingTimes[(int)effect] = duration;
    }

    private void UpdateStatusEffects()
    {
        for (int i = 0; i < statusEffectsRemainingTimes.Length; i++)
        {
            if (statusEffectsRemainingTimes[i] > 0)
            {
                statusEffectsRemainingTimes[i] -= Time.deltaTime;
            }
        }
    }

    public float GetDistanceToBase()
    {
        //this should return an already calculated distance value, maybe done in a custom coroutine every 0.05 seconds or more
        return 10f;
    }
}

public enum StatusEffect
{
    None,
    Freeze,
    Poison,
    Stun
}
