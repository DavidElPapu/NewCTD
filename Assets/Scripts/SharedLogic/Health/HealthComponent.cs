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

    public void ModifyHealth(float healthAmount)
    {
        currentHealth = Mathf.Clamp(currentHealth + healthAmount, 0, maxHealth);
    }
}
