using System;
using System.Collections.Generic;
using UnityEngine;

public class TempBaseEnemy : MonoBehaviour, IDamageable
{
    public event Action<GameObject> OnEnemyDeath;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] public float speed;
    private HealthComponent healthComponent;
    private int index;

    private void Awake()
    {
        index = 0;
        transform.LookAt(new Vector3(waypoints[index].position.x, transform.position.y, waypoints[index].position.z));
    }

    private void Update()
    {
        if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(waypoints[index].position.x, waypoints[index].position.z)) < .5)
        {
            if (index == waypoints.Count - 1)
            {
                //logica de daño y kaboom
            }
            else
            {
                index++;
                transform.LookAt(new Vector3(waypoints[index].position.x, transform.position.y, waypoints[index].position.z));
            }
        }
        else
        {
            transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime, Space.Self);

        }

    }

    private void OnDeath()
    {
        OnEnemyDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        healthComponent.ModifyHealth(damage);
        if (healthComponent.CurrentHealth < 0)
            OnDeath();
    }
}
