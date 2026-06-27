using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent (typeof(SphereCollider))]
public class TowerDetectionRange : MonoBehaviour
{
    [SerializeField] private SphereCollider detectionArea;
    public List<BaseEnemy> enemiesInRange = new List<BaseEnemy>();

    public void Initialize(float range)
    {
        detectionArea.radius = range;
        detectionArea.enabled = true;
    }

    public void Deactivate()
    {
        //For now there is no need to disable this component since it will not do anything without collider active
        //For now there is also no need to empty the enemy list since disabling the collider triggers ontriggerexit
        detectionArea.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BaseEnemy enemyScript))
        {
            enemiesInRange.Add(enemyScript);
            enemyScript.OnEnemyDeath += EnemyRemove;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //to avoid doing this for every object in range, a layer should be created for only enemies or objects, then the collider should only include those layers
        if (other.gameObject.TryGetComponent(out BaseEnemy enemyScript))
        {
            EnemyRemove(enemyScript);
        }
    }

    private void EnemyRemove(BaseEnemy enemyScript)
    {
        if (enemiesInRange.Contains(enemyScript))
        {
            enemiesInRange.Remove(enemyScript);
            enemyScript.OnEnemyDeath -= EnemyRemove;
        }
    }

    public BaseEnemy GetFirstEnemy()
    {
        BaseEnemy firstEnemy = null;
        float closestDistance = Mathf.Infinity;
        for (int i = 0; i < enemiesInRange.Count; i++)
        {
            if (enemiesInRange[i].GetDistanceToBase() < closestDistance)
            {
                closestDistance = enemiesInRange[i].GetDistanceToBase();
                firstEnemy = enemiesInRange[i];
            }
        }
        return firstEnemy;
    }
}
