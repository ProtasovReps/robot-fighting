using CharacterSystem.FighterParts;
using Interface;

namespace ArmorSystem
{
    public class NoTorsoArmor : ITorsoArmor
    {
        private readonly Torso _torso;

        public NoTorsoArmor(Torso torso)
        {
            _torso = torso;
        }
        
        public void AcceptDamage(float damage)
        {
            _torso.AcceptDamage(damage);
        }
    }
}