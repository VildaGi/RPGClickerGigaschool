using System;
using System.Collections.Generic;
using Game.Configs.SkillsConfig;
using Game.Enemy;
using Global.SaveSystem.SavableObjects;

namespace Game.Skills
{
    public class SkillSystem
    {
        private SkillScope _scope;
        private SkillsConfig _skillsConfig;

        private Dictionary<SkillTrigger, List<Skill>> _skillByTrigger;
        
        
        public SkillSystem(OpenedSkills openedSkills, SkillsConfig skillsConfig, EnemyManager enemyManager, StatusManager.StatusManager statusManager)
        {
            _skillsConfig = skillsConfig;
            _skillByTrigger = new();
            _scope = new()
            {
                EnemyManager = enemyManager,
                StatusManager = statusManager
            };
            
            foreach (var skill in openedSkills.Skills)
            {
                RegisterSkill(skill);
            }
        }

        public void InvokeTrigger(SkillTrigger trigger)
        {
            if (!_skillByTrigger.ContainsKey(trigger)) return;
            
            var skiillsToActivate = _skillByTrigger[trigger];
            foreach (var skill in skiillsToActivate)
            {
                skill.SkillProcess();
            }
        }
        private void RegisterSkill(SkillWithLevel skill)
        {
            var skillData = _skillsConfig.GetSkillData(skill.Id, skill.Level);
            
            var skillType = Type.GetType($"Game.Skills.SkillVariants.{skill.Id}");
            if (skillType == null)
            {
                throw new Exception($"Skill with id {skill.Id} not found");
            }
            // делаем черную магию: по строке делаем класс
            //var skillInstance = Activator.CreateInstance(skillType) as Skill;
            //сделали рефакторинг
            
            if (Activator.CreateInstance(skillType) is not Skill skillInstance)
            {
                throw new Exception($"Cannot creat skill with id {skill.Id}");
            }
            skillInstance.Initialize(_scope, skillData);

            if (!_skillByTrigger.ContainsKey(skillData.Trigger))
            {
                _skillByTrigger[skillData.Trigger] = new();
            }
            
            _skillByTrigger[skillData.Trigger].Add(skillInstance);
            skillInstance.OnSkillRegistered();
            
        }
    }
}