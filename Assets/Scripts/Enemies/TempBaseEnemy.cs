using System;
using System.Collections.Generic;
using UnityEngine;

public class TempBaseEnemy : MonoBehaviour, IEnemyHealth
{
    public event Action<GameObject> OnDeath;
    [SerializeField] List<Transform> waypoints;
    [SerializeField] public float speed;
    private int health, index;

    private void Awake()
    {
        health = 100;
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

    public void DealDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    public int GetCurrentHealth()
    {
        throw new NotImplementedException();
    }

    public void Heal(int healAmount)
    {
        throw new NotImplementedException();
    }

    private void Die()
    {
        OnDeath?.Invoke(gameObject);
        Destroy(gameObject);
    }
}
