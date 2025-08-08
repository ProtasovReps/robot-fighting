using System;
using ArmorSystem;
using Extensions.Exceptions;
using Interface;

namespace CharacterSystem.FighterParts
{
    public class Torso : FighterPart
    {
        private ITorsoArmor _torsoArmor;

        public void SetArmor(ITorsoArmor torsoArmor)
        {
            if (torsoArmor == null)
                throw new ArgumentNullException(nameof(torsoArmor));
            
            _torsoArmor = torsoArmor;
        }
        
        protected override float ValidateDamage(float damage)
        {
            if (_torsoArmor == null)
            {
                _torsoArmor = new NoTorsoArmor();
                // throw new ArmorNotSetException(nameof(_torsoArmor));
            }
            
            return _torsoArmor.ValidateDamage(damage);
        }
    }
}