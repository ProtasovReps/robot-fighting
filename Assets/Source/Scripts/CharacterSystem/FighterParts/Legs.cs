using System;
using ArmorSystem;
using Extensions.Exceptions;
using Interface;

namespace CharacterSystem.FighterParts
{
    public class Legs : FighterPart
    {
        private ILegsArmor _legsArmor;

        public void SetArmor(ILegsArmor legsArmor)
        {
            if (legsArmor == null)
                throw new ArgumentNullException(nameof(legsArmor));
            
            _legsArmor = legsArmor;
        }
        
        protected override float ValidateDamage(float damage)
        {
            if (_legsArmor == null) // mock
            {
                _legsArmor = new NoLegsArmor();
                // throw new ArmorNotSetException(nameof(_legsArmor));
            }
            
            return _legsArmor.ValidateDamage(damage);
        }
    }
}