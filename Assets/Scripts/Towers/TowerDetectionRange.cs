using UnityEngine;
using System.Collections.Generic;
using System;

[RequireComponent (typeof(SphereCollider))]
public class TowerDetectionRange : MonoBehaviour
{
    public List<BaseEnemy> enemiesInRange = new List<BaseEnemy>();
    [SerializeField] private SphereCollider detectionArea;

    private void Awake()
    {
        TryGetComponent(out detectionArea);
    }

    public void Initialize(float range, LayerMask detectionLayers)
    {
        detectionArea.radius = range;
        LayerMask excludedLayers = ~detectionLayers;
        detectionArea.excludeLayers = excludedLayers;
        enemiesInRange.Clear();
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
        enemiesInRange.Remove(enemyScript);
        enemyScript.OnEnemyDeath -= EnemyRemove;
    }

    public BaseEnemy GetFirstEnemy()
    {
        if (enemiesInRange.Count <= 0) return null;
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
