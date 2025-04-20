using Game.Enemy;
using Game.Skills.Data;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
    [Preserve]
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
            _enemyManager.IncreasePlayerDamage(_skillData.Value);
        }
    }
}