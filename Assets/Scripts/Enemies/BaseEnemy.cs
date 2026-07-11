using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthComponent))]
public class BaseEnemy : MonoBehaviour, IDamageable
{
    public event Action<BaseEnemy> OnEnemyDeath;
    private EnemyDataSO data;
    private HealthComponent healthComponent;
    private MeshRenderer meshRenderer;
    private Rigidbody rb;
    private List<Transform> waypoints;
    private float[] statusEffectsRemainingTimes;
    private float distanceToWaypoint;
    private int currentWaypoint;

    private void Awake()
    {
        TryGetComponent(out healthComponent);
        TryGetComponent(out meshRenderer);
        TryGetComponent(out rb);
        //This array need to be updated everytime a new status effect type is added to the enum, (ignoring the first element "None")
        statusEffectsRemainingTimes = new float[3];
    }

    public void Initialize(EnemyDataSO newData, List<Transform> newWaypoints)
    {
        data = newData;
        healthComponent.SetMaxHealth(data.health, true);
        meshRenderer.sharedMaterial = data.bodyMaterial;
        waypoints = newWaypoints;
        for (int i = 0; i < statusEffectsRemainingTimes.Length; i++)
        {
            statusEffectsRemainingTimes[i] = 0f;
        }
        currentWaypoint = 0;
        if (data.isInvisible)
            gameObject.layer = LayerMask.NameToLayer("InvisibleEnemy");
        enabled = true;
    }

    private void Update()
    {
        UpdateStatusEffects();
    }

    private void FixedUpdate()
    {
        WalkToWaypoint();
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
        if (effect == StatusEffect.None) return;

        //If the effect time is greater as the current effect time, it sets the greater time
        if (duration > statusEffectsRemainingTimes[(int)effect - 1])
            statusEffectsRemainingTimes[(int)effect - 1] = duration;
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

    private void WalkToWaypoint()
    {
        distanceToWaypoint = Vector3.Distance(transform.position, waypoints[currentWaypoint].position);

        //If the enemy reached a waypoint (is closer than 0.5 units from it)
        if (distanceToWaypoint < 0.5f)
        {
            if (currentWaypoint >= waypoints.Count - 1)
            {
                //If the enemy reached the final waypoint, stops and attacks
            }
            else
            {
                //Looks at the next waypoint
                currentWaypoint++;
                transform.LookAt(waypoints[currentWaypoint].position);
            }
        } 
        else
        {
            Vector3 newPos = Vector3.MoveTowards(transform.position, waypoints[currentWaypoint].position, data.speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);
        }
    }

    public float GetDistanceToBase()
    {
        //the distance to base is the distance to the currentWaypoint + the waypoint value, the further the waypoint is to the base, the greater it is
        return distanceToWaypoint + (((waypoints.Count - 1) - currentWaypoint) * 100f);
    }
}

public enum StatusEffect
{
    None,
    Freeze,
    Poison,
    Stun
}
