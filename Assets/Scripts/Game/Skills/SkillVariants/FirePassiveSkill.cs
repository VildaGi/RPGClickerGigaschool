using Game.Enemy;
using Game.Skills.Data;
using Game.StatusManager;
using UnityEngine.Scripting;
using Random = System.Random;

namespace Game.Skills.SkillVariants
{
    [Preserve]
    public class FirePassiveSkill : Skill
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
                _statusManager.AddStatus(new ()
                {
                    StatusName = "Fire Passive",
                    statusType = StatusType.DoT,
                    StatusValue = _skillData.Value,
                    Time = _skillData.Level * 3,
                });
            }
        }
    }
}