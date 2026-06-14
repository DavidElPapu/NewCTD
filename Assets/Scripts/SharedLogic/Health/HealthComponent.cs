using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    private float currentHealth, maxHealth;
    public float CurrentHealth => currentHealth;

    public void SetMaxHealth(float newMaxHealth, bool fillHealth)
    {
        maxHealth = newMaxHealth;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
        if (fillHealth)
            currentHealth = maxHealth;
    }

    public void IncreaseHealth(float healthAmount)
    {
        currentHealth += healthAmount;
        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    public void DecreaseHealth(float healthAmount)
    {
        currentHealth -= healthAmount;
        if (currentHealth < 0)
            currentHealth = 0;
    }
}
