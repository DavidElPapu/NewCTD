using System;
using UnityEngine;

public class TempBaseEnemy : MonoBehaviour, IEnemyHealth
{
    public event Action<GameObject> OnDeath;
    public int health;

    private void Awake()
    {
        health = 10;
    }

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            OnDeath?.Invoke(gameObject);
    }

    public int GetCurrentHealth()
    {
        throw new NotImplementedException();
    }

    public void Heal(int healAmount)
    {
        throw new NotImplementedException();
    }
}
