using UnityEngine;
using System.Collections.Generic;
using System;

public class TowerDetectionRange : MonoBehaviour
{
    //Consider changing the hashset from gameobjects to enemy monobehavour script, since it might decrease trygetcomponent calls
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

    private GameObject GetFirstEnemy()
    {
        //this whole thing might need to change to return enemy script instead and get first enemy
        GameObject firstEnemy = null;
        float closestDistance = 1000f;
        foreach (GameObject enemy in enemiesInRange)
        {
            if(enemy.transform.position.y < closestDistance)
            {
                firstEnemy = enemy;
            }
        }
        return firstEnemy;
    }
}
