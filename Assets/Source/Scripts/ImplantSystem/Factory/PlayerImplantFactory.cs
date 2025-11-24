using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using UI.Store;
using UnityEngine;
using YG.Saver;

namespace ImplantSystem.Factory
{
    public class PlayerImplantFactory : ImplantFactory 
    {
        protected override AttackImplant[] GetImplants()
        {
            PlayerImplantSave implantSave = new();

            AttackImplant upAttackImplant = implantSave.Get(AttackType.UpAttack);
            AttackImplant downAttackImplant = implantSave.Get(AttackType.DownAttack);
            AttackImplant superAttackImplant = implantSave.Get(AttackType.Super);
            
            return new[] { upAttackImplant, downAttackImplant, superAttackImplant};
        }

        protected override void AddAttackSides(Dictionary<AttackPart, AttackPartSide> partSides)
        {
            partSides.Add(AttackPart.Hands, AttackPartSide.LeftHand);
            partSides.Add(AttackPart.Legs, AttackPartSide.RightLeg);
        }
    }
}