using Game.Enemy;
using Game.Skills.Data;
using UnityEngine.Scripting;
using Random = System.Random;

namespace Game.Skills.SkillVariants
{
    [Preserve]
    public class BonusBountySkill : Skill
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
            var rnd = new Random();
            if (rnd.Next(0, 100) < 2 * _skillData.Level)
            {
                _enemyManager.AddExtraCoins((int)_skillData.Value);
            }
        }
    }
}