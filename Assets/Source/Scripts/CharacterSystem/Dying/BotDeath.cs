using CharacterSystem.CharacterHealth;
using HitSystem;
using Interface;

namespace CharacterSystem.Dying
{
    public class BotDeath : Death
    {
        public BotDeath(HitReader hitReader, Health health, IConditionAddable conditionAddable) 
            : base(hitReader, health, conditionAddable)
        {
        }
    }
}