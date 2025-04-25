using Game.Elements;
using Game.Enemy;
using Game.Skills.Data;
using Game.StatusManager;
using UnityEngine.Scripting;
using Random = System.Random;

namespace Game.Skills.SkillVariants
{
    [Preserve]
    public class AirActiveSkill : Skill
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
            for (int i = 0; i < _skillData.Value; i++)
            {
                _enemyManager.DamageCurrentEnemy(ElementType.Air);
            }
        }
    }
}