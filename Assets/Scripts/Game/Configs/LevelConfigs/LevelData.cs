using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Configs.LevelConfigs {
    [Serializable]
    public struct LevelData {
        public int Location;
        public int LevelNumber;
        public List<Sprite> LevelBackgrounds;
        public List<EnemySpawnData> Enemies;
        public int Reward;
    }
}