using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public event Action WavesCompleted;
    [SerializeField] private List<Transform> mapWaypoints;
    [SerializeField] private LevelEnemyWavesConfigSO levelData;
    [SerializeField] private GameObject enemyPrefab;
    private List<BaseEnemy> aliveEnemies; 
    private Coroutine waveCoroutine, levelCoroutine;
    private float enemySpawnDistanceOffset;

    private void Awake()
    {
        aliveEnemies = new List<BaseEnemy>();
        waveCoroutine = null;
        levelCoroutine = null;
        enemySpawnDistanceOffset = 1f;
    }

    public void StartEnemyWaves()
    {
        if (levelCoroutine != null)
            StopCoroutine(levelCoroutine);
        if (aliveEnemies.Count > 0)
            aliveEnemies.Clear();
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
            {
                enemyScript.Initialize(enemiesToSpawn.enemyData, mapWaypoints);
                aliveEnemies.Add(enemyScript);
                enemyScript.OnEnemyDeath += EnemyDied;
            }
        }
    }

    private IEnumerator RunEnemyWaves()
    {
        //Iterates through every wave to spawn the enemies
        for (int i = 0; i < levelData.enemyWaves.Count; i++)
        {
            EnemyWave currentWave = levelData.enemyWaves[i];

            //Waits this wave delay before spawning the enemies
            Debug.Log("Preparation time");
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

    public void EnemyDied(BaseEnemy enemy)
    {
        aliveEnemies.Remove(enemy);
        enemy.OnEnemyDeath -= EnemyDied;
        if (aliveEnemies.Count == 0 && levelCoroutine == null && waveCoroutine == null)
        {
            //If this was the last wave and the last enemy, all waves are offitially done
            WavesCompleted?.Invoke();
        }
    }

    private void OnDisable()
    {
        //Just in case this ensures no memory leaks
        StopAllCoroutines();
        waveCoroutine = null;
        levelCoroutine = null;
    }
}
