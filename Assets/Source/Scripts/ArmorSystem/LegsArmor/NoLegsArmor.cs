using CharacterSystem.FighterParts;
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
        
        public void AcceptDamage(float damage)
        {
            _legs.AcceptDamage(damage);
        }
    }
}