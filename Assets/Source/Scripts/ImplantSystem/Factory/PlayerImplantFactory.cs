using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem.Factory
{
    public class PlayerImplantFactory : ImplantFactory // тут все получать из сейва в будущем
    {
        [SerializeField] private AttackImplant _upAttackImplant;
        [SerializeField] private AttackImplant _downAttackImplant;
        [SerializeField] private AttackImplant _superAttackImplant;

        protected override AttackImplant[] GetImplants()
        {
            return new[] { _upAttackImplant, _downAttackImplant, _superAttackImplant };
        }

        protected override void AddAttackSides(Dictionary<AttackPart, AttackPartSide> partSides)
        {
            partSides.Add(AttackPart.Hands, AttackPartSide.LeftHand);
            partSides.Add(AttackPart.Legs, AttackPartSide.RightLeg);
        }
    }
}