using Game.Elements;
using Game.Enemy;
using Game.Skills.Data;
using Game.StatusManager;
using UnityEngine.Scripting;

namespace Game.Skills.SkillVariants
{
    [Preserve]
    public class FireActiveSkill : Skill
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
            _enemyManager.DamageCurrentEnemy(_skillData.Value, ElementType.Fire);
            _statusManager.AddStatus(new ()
            {
                StatusName = "Fire Active",
                statusType = StatusType.DoT,
                StatusElementType = ElementType.Fire,
                StatusValue = _skillData.Value/10,
                Time = _skillData.Level * 3,
            });
        }
    }
}