using HealthSystem;
using HitSystem;
using Interface;

namespace FightingSystem.Dying
{
    public class BotDeath : Death
    {
        public BotDeath(HitReader hitReader, Health health, IConditionAddable conditionAddable) 
            : base(hitReader, health, conditionAddable)
        {
        }
    }
}