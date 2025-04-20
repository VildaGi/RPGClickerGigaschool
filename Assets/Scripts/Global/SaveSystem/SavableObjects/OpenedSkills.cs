using System;
using System.Collections.Generic;
using UnityEngine;

namespace Global.SaveSystem.SavableObjects
{ 
    [Serializable]
    public class OpenedSkills : ISavable
    { 
        public List<SkillWithLevel> Skills = new();

        public SkillWithLevel GetSkillWithLevel(string skillId)
        {
            foreach (var skillWithLevel in Skills)
            {
                if (skillWithLevel.Id == skillId)
                {
                    return skillWithLevel;
                }
            }
            
            var newSkill = new SkillWithLevel() {
                Id = skillId,
                Level = 0
            };
            Skills.Add(newSkill);
            return newSkill;
        }
    }
}