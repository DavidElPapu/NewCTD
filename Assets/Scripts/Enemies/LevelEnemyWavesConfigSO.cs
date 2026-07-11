using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelEnemyWavesConfig", menuName = "Enemies/LevelEnemyWavesConfigSO")]
public class LevelEnemyWavesConfigSO : ScriptableObject
{
    public List<EnemyWave> enemyWaves;
}

[System.Serializable]
public struct EnemyWave
{
    public List<EnemySpawn> enemiesToSpawn;
    public float waveDelay;
}

[System.Serializable]
public struct EnemySpawn
{
    public EnemyDataSO enemyData;
    public int amount;
    public float spawnDelay;
}
