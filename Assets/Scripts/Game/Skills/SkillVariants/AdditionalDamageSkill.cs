using Game.Enemy;
using Game.Skills.Data;

namespace Game.Skills.SkillVariants
{
    public class AdditionalDamageSkill : Skill
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
            _enemyManager.DamageCurrentEnemy(_skillData.Value);
        }
    }
}