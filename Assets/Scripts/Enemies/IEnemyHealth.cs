using System;
using UnityEngine;

public interface IEnemyHealth
{
    event Action<GameObject> OnDeath;
    int GetCurrentHealth();
    void DealDamage(int damage);
    void Heal(int healAmount);
}
