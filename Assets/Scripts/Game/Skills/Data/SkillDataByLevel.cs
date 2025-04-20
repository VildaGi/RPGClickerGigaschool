using System;
using Game.Elements;

namespace Game.Skills.Data
{
    [Serializable]
    public struct SkillDataByLevel
    {
        public int Level;
        public ElementType ElementType;
        public float Value;
        public SkillTrigger Trigger;
        public float TriggerValue;
        public int Cost;
    }
}