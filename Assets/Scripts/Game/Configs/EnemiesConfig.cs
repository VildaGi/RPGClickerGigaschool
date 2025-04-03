using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu(fileName = "EnemiesConfig", menuName = "Configs/EnemiesConfig")]
    public class EnemiesConfig : ScriptableObject
    {
        public Enemy.Enemy EnemyPrefab;
        public List<EnemyViewData> Enemies;

        public EnemyViewData GetEnemy(string id)
        {
            foreach (var enemyData in Enemies)
            {
                if (enemyData.Id == id) return enemyData;
            }
            
            Debug.LogError($"Not found enemy with id: {id}");
            return default;
        }
    }
}