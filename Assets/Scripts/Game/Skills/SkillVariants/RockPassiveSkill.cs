using Game.Enemy;
using Game.Skills.Data;
using Game.StatusManager;
using UnityEngine.Scripting;
using Random = System.Random;

namespace Game.Skills.SkillVariants
{
    [Preserve]
    public class RockPassiveSkill : Skill
    {
        private SkillDataByLevel _skillData;
        private EnemyManager _enemyManager;
        private StatusManager.StatusManager _statusManager;

        public override void Initialize(SkillScope scope, SkillDataByLevel skillData)
        {
            _skillData = skillData;
            _enemyManager = scope.EnemyManager;
            _statusManager = scope.StatusManager;
        }

        public override void SkillProcess()
        {
            if (_skillData.ElementType != _enemyManager.GetElementType()) return;
            var rnd = new Random();
            if (rnd.Next(0, 100) < 5 * _skillData.Level)
            {
                _enemyManager.DamageCurrentEnemy(_skillData.Value);
            }
        }
    }
}