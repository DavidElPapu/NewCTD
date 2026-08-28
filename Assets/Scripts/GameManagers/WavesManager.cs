using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public event Action WavesCompleted;
    public event Action BaseDestroyed;
    [SerializeField] private List<Transform> mapWaypoints;
    [SerializeField] private LevelEnemyWavesConfigSO levelData;
    [SerializeField] private GameObject enemyPrefab;
    private List<BaseEnemy> aliveEnemies; 
    private Coroutine waveCoroutine, levelCoroutine;
    private float enemySpawnDistanceOffset;
    private int currentWave, maxWaves;

    [Header("MapBase")]
    [SerializeField] private float maxBaseHealth;
    private float currentBaseHealth;

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
        maxWaves = levelData.enemyWaves.Count;
        currentBaseHealth = maxBaseHealth;
        levelCoroutine = StartCoroutine(RunEnemyWaves());
        UIManager.singleton.waveUI.ShowWaveUI(maxBaseHealth);
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
                UIManager.singleton.waveUI.DisplayEnemiesLeftText(aliveEnemies.Count);
            }
        }
    }

    private IEnumerator RunEnemyWaves()
    {
        //Iterates through every wave to spawn the enemies
        for (currentWave = 0; currentWave < maxWaves; currentWave++)
        {
            EnemyWave currentWaveStruct = levelData.enemyWaves[currentWave];

            //Waits this wave delay before spawning the enemies
            if (aliveEnemies.Count == 0)
                UIManager.singleton.waveUI.DisplayBreakText(currentWave, maxWaves);
            UIManager.singleton.waveUI.ResetWaveTimerSlider(currentWaveStruct.waveDelay);
            float currentWaveTimer = 0f;
            while (currentWaveTimer < currentWaveStruct.waveDelay)
            {
                currentWaveTimer += Time.deltaTime;
                UIManager.singleton.waveUI.UpdateWaveTimerSlider(currentWaveTimer);
                yield return null;
            }
            UIManager.singleton.waveUI.DisplayCurrentWaveText(currentWave + 1, maxWaves);
            //Starts spawning the enemies of the wave
            if (waveCoroutine != null)
                StopCoroutine(waveCoroutine);
            waveCoroutine = StartCoroutine(SpawnWaveEnemies(currentWaveStruct));
        }
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
        if (enemy.GetDistanceToBase() <= 0.5f)
        {
            //If the enemy reached the last waypoint, it died and here applies the damage equal to its health
            currentBaseHealth -= enemy.GetCurrentHealth();
            UIManager.singleton.waveUI.UpdateBaseHealthSlider(currentBaseHealth);
            if (currentBaseHealth <= 0)
                BaseDestroyed?.Invoke();
        }
        if (aliveEnemies.Count == 0)
        {
            if (currentWave == maxWaves - 1)
            {
                //If this was the last wave and the last enemy, all waves are offitially done
                UIManager.singleton.waveUI.DisplayBreakText(maxWaves, maxWaves);
                WavesCompleted?.Invoke();
            }
            else
                UIManager.singleton.waveUI.DisplayBreakText(currentWave, maxWaves);
        }
        else
            UIManager.singleton.waveUI.DisplayEnemiesLeftText(aliveEnemies.Count);
    }

    private void OnDisable()
    {
        //Just in case this ensures no memory leaks
        StopAllCoroutines();
        waveCoroutine = null;
        levelCoroutine = null;
    }
}
