using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using YG.Saver;

namespace ImplantSystem.Factory
{
    public class PlayerImplantFactory : ImplantFactory
    {
        private ImplantPlaceHolderStash _stash;
        
        public void Initialize(ImplantPlaceHolderStash stash)
        {
            _stash = stash;
        }
        
        protected override AttackImplant[] GetImplants()
        {
            PlayerImplantSave implantSave = new();

            AttackImplant upAttackImplant = implantSave.Get(AttackType.UpAttack).AttackImplant;
            AttackImplant downAttackImplant = implantSave.Get(AttackType.DownAttack).AttackImplant;
            AttackImplant superAttackImplant = implantSave.Get(AttackType.Super).AttackImplant;
            
            return new[] { upAttackImplant, downAttackImplant, superAttackImplant};
        }

        protected override ImplantPlaceHolderStash GetStash()
        {
            return _stash;
        }

        protected override void AddAttackSides(Dictionary<AttackPart, AttackPartSide> partSides)
        {
            partSides.Add(AttackPart.Hands, AttackPartSide.LeftHand);
            partSides.Add(AttackPart.Legs, AttackPartSide.RightLeg);
        }
    }
}