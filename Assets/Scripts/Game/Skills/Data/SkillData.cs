using System;
using System.Collections.Generic;
using System.Linq;

namespace Game.Skills.Data
{
    [Serializable]
    public struct SkillData
    {
        public string SkillId;
        public List<SkillDataByLevel> SkillLevels;

        public SkillDataByLevel GetSkillByLevel(int level)
        {
            return SkillLevels.Find(x => x.Level == level);
        }
        
        public bool IsMaxLevel(int level)
        {
            return SkillLevels.Max(x => x.Level) == level;
        }
    }
}