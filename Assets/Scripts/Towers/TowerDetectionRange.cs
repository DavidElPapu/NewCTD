using UnityEngine;
using System.Collections.Generic;
using System;

public class TowerDetectionRange : MonoBehaviour, ITowerComponent
{
    public List<BaseEnemy> enemiesInRange = new List<BaseEnemy>();
    [SerializeField] private SphereCollider detectionArea;

    public void SetupData(BaseTowerSO newData)
    {
        //for now uses range1, maybe later will use a custom one
        detectionArea.radius = newData.range1;
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
