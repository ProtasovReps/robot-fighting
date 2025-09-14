using HealthSystem;
using HitSystem;
using Interface;

namespace FightingSystem.Dying
{
    public class PlayerDeath : Death
    {
        public PlayerDeath(HitReader hitReader, Health health, IConditionAddable conditionAddable)
            : base(hitReader, health, conditionAddable)
        {
        }
    }
}