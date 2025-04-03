using Unity.VisualScripting;

namespace Game.Skills
{
    public enum SkillTrigger
    {
        OnDamage = 1,
        OnDead = 2,
        OnTime = 3,
        OnTimerLeft = 4,
        OnStart = 5, //скилл который вещается в начале и висит всю игру
    }
}