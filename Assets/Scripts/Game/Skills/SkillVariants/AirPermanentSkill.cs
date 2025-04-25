using Game.Elements;
using Game.Enemy;
using Game.Skills.Data;
using Game.StatusManager;
using UnityEngine.Scripting;
using Random = System.Random;

namespace Game.Skills.SkillVariants
{
    [Preserve]
    public class AirPermanentSkill : Skill
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
            var rnd = new Random();
            if (rnd.Next(0, 100) < 5 * _skillData.Level)
            {
                _statusManager.AddStatus(new ()
                {
                    StatusName = "Air Permanent",
                    statusType = StatusType.Multiplier,
                    StatusElementType = ElementType.Air,
                    StatusValue = _skillData.Value,
                    Time = _skillData.Level * 3,
                });
            }
        }
    }
}