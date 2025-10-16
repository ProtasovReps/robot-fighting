using FightingSystem.AttackDamage;
using HitSystem.FighterParts;
using Interface;

namespace ArmorSystem
{
    public class NoLegsArmor : ILegsArmor
    {
        private readonly Legs _legs;

        public NoLegsArmor(Legs legs)
        {
            _legs = legs;
        }
        
        public void AcceptDamage(Damage damage)
        {
            _legs.AcceptDamage(damage);
        }
    }
}