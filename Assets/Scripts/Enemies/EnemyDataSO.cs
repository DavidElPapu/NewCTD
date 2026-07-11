using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemies/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    [Header("Stats")]
    public float health;
    public float speed;
    public bool isInvisible;
    [Header("Visuals")]
    public Material bodyMaterial;
}
