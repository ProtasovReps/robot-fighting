using CharacterSystem.CharacterHealth;
using HitSystem;
using Interface;

namespace CharacterSystem.Dying
{
    public class PlayerDeath : Death
    {
        public PlayerDeath(HitReader hitReader, Health health, IConditionAddable conditionAddable)
            : base(hitReader, health, conditionAddable)
        {
        }
    }
}