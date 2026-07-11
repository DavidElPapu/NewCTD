using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> mapWaypoints;
    [SerializeField] private LevelEnemyWavesConfigSO levelData;
    [SerializeField] private GameObject enemyPrefab;
    private Coroutine waveCoroutine, levelCoroutine;
    private float enemySpawnDistanceOffset;

    private void Awake()
    {
        enemySpawnDistanceOffset = 1f;
    }

    private void Start()
    {
        if (levelCoroutine != null)
            StopCoroutine(levelCoroutine);
        levelCoroutine = StartCoroutine(RunEnemyWaves());
    }

    private void SpawnEnemies(EnemySpawn enemiesToSpawn)
    {
        //Spawns the amount of enemies of the type, separated by a fixed distance
        for (int i = 0; i < enemiesToSpawn.amount; i++)
        {
            Vector3 offsetPos = enemySpawnDistanceOffset * i * -mapWaypoints[0].forward;
            GameObject newEnemy = Instantiate(enemyPrefab, mapWaypoints[0].position + offsetPos, mapWaypoints[0].rotation);
            if (newEnemy.TryGetComponent(out BaseEnemy enemyScript))
                enemyScript.Initialize(enemiesToSpawn.enemyData, mapWaypoints);
        }
    }

    private IEnumerator RunEnemyWaves()
    {
        //Iterates through every wave to spawn the enemies
        for (int i = 0; i < levelData.enemyWaves.Count; i++)
        {
            EnemyWave currentWave = levelData.enemyWaves[i];

            //Waits this wave delay before spawning the enemies
            float currentWaveTimer = 0f;
            while (currentWaveTimer < currentWave.waveDelay)
            {
                currentWaveTimer += Time.deltaTime;
                yield return null;
            }

            Debug.Log("Wave " + (i + 1) + " Starts NOW");
            //Starts spawning the enemies of the wave
            if (waveCoroutine != null)
                StopCoroutine(waveCoroutine);
            waveCoroutine = StartCoroutine(SpawnWaveEnemies(currentWave));
        }
        Debug.Log("Waves ended, now I wait till all enemies are dead to finish");
        levelCoroutine = null;
    }

    private IEnumerator SpawnWaveEnemies(EnemyWave enemyWave)
    {
        //Iterates through the enemies in the wave and spawn them
        for (int i = 0; i < enemyWave.enemiesToSpawn.Count; i++)
        {
            EnemySpawn enemySpawn = enemyWave.enemiesToSpawn[i];

            //Waits for the enemy spawn delay before spawning it
            float spawnTimer = 0f;
            while (spawnTimer < enemySpawn.spawnDelay)
            {
                spawnTimer += Time.deltaTime;
                yield return null;
            }

            //Spawns the enemy(s)
            SpawnEnemies(enemySpawn);
        }
        waveCoroutine = null;
    }

    private void OnDisable()
    {
        //Just in case this ensures no memory leaks
        StopAllCoroutines();
        waveCoroutine = null;
        levelCoroutine = null;
    }
}
