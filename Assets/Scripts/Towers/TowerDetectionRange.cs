using UnityEngine;
using System.Collections.Generic;
using System;

public class TowerDetectionRange : MonoBehaviour
{
    public event Action OnEnemyInRange;
    //Consider changing the hashset from gameobjects to IEnemyHealth, since it might decrease trygetcomponent calls
    public HashSet<GameObject> enemiesInRange;

    private void Awake()
    {
        enemiesInRange = new HashSet<GameObject>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IEnemyHealth health))
        {
            enemiesInRange.Add(other.gameObject);
            health.OnDeath += EnemyRemove;
            if (enemiesInRange.Count == 1)
                OnEnemyInRange?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //to avoid doing this for every object in range, a layer should be created for only enemies or objects, then the collider should only include those layers
        EnemyRemove(other.gameObject);
    }

    private void EnemyRemove(GameObject enemy)
    {
        if (enemiesInRange.Remove(enemy))
        {
            if (enemy.TryGetComponent(out IEnemyHealth health))
                health.OnDeath -= EnemyRemove;
        }
    }
}
