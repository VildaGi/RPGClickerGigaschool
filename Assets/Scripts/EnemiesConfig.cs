using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig")]
public class EnemiesConfig : ScriptableObject
{
    public Enemy EnemyPrefab;
    public List<EnemyData> Enemies;
}