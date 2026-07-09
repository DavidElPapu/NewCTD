using System.Collections.Generic;
using UnityEngine;

public class TemporalEnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public List<Transform> mapWaypoints;
    public List<EnemySpawn> enemiesToSpawn;
    private int currentEnemySpawn;
    private float currentSpawnTimer;

    private void Awake()
    {
        currentEnemySpawn = 0;
        currentSpawnTimer = 0f;
    }

    private void Update()
    {
        if (currentEnemySpawn >= enemiesToSpawn.Count) return;
        if (currentSpawnTimer >= enemiesToSpawn[currentEnemySpawn].spawnDelay)
        {
            GameObject newEnemy = Instantiate(enemyPrefab, mapWaypoints[0].position, mapWaypoints[0].rotation);
            if (newEnemy.TryGetComponent(out BaseEnemy enemyScript))
                enemyScript.Initialize(enemiesToSpawn[currentEnemySpawn].enemyData, mapWaypoints);
            currentEnemySpawn++;
            currentSpawnTimer = 0f;
        }
        else
        {
            currentSpawnTimer += Time.deltaTime;
        }
    }

}

[System.Serializable]
public struct EnemySpawn
{
    public EnemyDataSO enemyData;
    public float spawnDelay;
}
