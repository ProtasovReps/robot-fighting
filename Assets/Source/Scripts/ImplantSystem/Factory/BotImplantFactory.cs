using System.Collections.Generic;
using Extensions;
using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem.Factory
{
    public class BotImplantFactory : ImplantFactory
    {
        private readonly List<AttackImplant> _attackImplants = new();

        [SerializeField] private ImplantPlaceHolderStash _placeHolderStash;        
        [SerializeField] private AttackImplant _upAttackImplant;
        [SerializeField] private AttackImplant _downAttackImplant;
        [SerializeField] private AttackImplant _specialAttackImplant;
        
        protected override AttackImplant[] GetImplants()
        {
            AddImplant(_upAttackImplant);
            AddImplant(_downAttackImplant);
            AddImplant(_specialAttackImplant);
            return _attackImplants.ToArray();
        }

        protected override ImplantPlaceHolderStash GetStash()
        {
            return _placeHolderStash;
        }

        protected override void AddAttackSides(Dictionary<AttackPart, AttackPartSide> partSides)
        {
            partSides.Add(AttackPart.Hands, AttackPartSide.RightHand);
            partSides.Add(AttackPart.Legs, AttackPartSide.LeftLeg);
        }

        protected void AddImplant(AttackImplant attackImplant)
        {
            _attackImplants.Add(attackImplant);
        }
    }
}