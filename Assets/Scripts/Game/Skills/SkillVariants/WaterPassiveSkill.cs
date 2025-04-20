using System;
using Game.Enemy;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
    [Preserve]
    public class WaterPassiveSkill : Skill
    {
        private SkillDataByLevel _skillData;
        private EnemyManager _enemyManager;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData)
        {
            _skillData = skillData;
            _enemyManager = scope.EnemyManager;
        }

        public override void SkillProcess()
        {
            if (_skillData.ElementType != _enemyManager.GetElementType()) return;
            var rnd = new Random();
            if (rnd.Next(0, 100) < 7 * _skillData.Level)
            {
                _enemyManager.DamageCurrentEnemy(_skillData.Value);
            }
        }
    }
}