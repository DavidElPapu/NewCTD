using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Game/LevelDataSO")]
public class LevelDataSO : ScriptableObject
{
    public int levelIndex;
    public GameObject mapPrefab;
    public LevelEnemyWavesConfigSO enemyWavesConfigSO;
    public IngredientSO unlockedIngredientSO;
}
