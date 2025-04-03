using System.Collections.Generic;
using Game.Skills.Data;
using UnityEngine;

namespace Game.Configs.SkillsConfig
{
    [CreateAssetMenu(fileName = "SkillsConfig", menuName = "Configs/SkillsConfig")]
    public class SkillsConfig : ScriptableObject
    {
        public List<SkillData> Skills;
        private Dictionary<string, Dictionary<int, SkillDataByLevel>> _skillDataByLevelMap;

        public SkillDataByLevel GetSkillData(string skillId, int level)
        {
            if (_skillDataByLevelMap == null || _skillDataByLevelMap.Count == 0)
            {
                FillSkillDataMao();
            }
            
            return _skillDataByLevelMap[skillId][level];
        }

        private void FillSkillDataMao()
        {
            _skillDataByLevelMap = new();
            foreach (var skillData in Skills)
            {
                if (!_skillDataByLevelMap.ContainsKey(skillData.SkillId))
                {
                    _skillDataByLevelMap[skillData.SkillId] = new();
                }
                foreach (var skillDataByLevel in skillData.SkillLevels)
                {
                    _skillDataByLevelMap[skillData.SkillId][skillDataByLevel.Level] = skillDataByLevel; 
                }
            }
        }
    }
}