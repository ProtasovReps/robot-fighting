using System.Collections.Generic;
using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem.Factory
{
    public class BotImplantFactory : ImplantFactory
    {
        private readonly List<AttackImplant> _attackImplants = new();

        [SerializeField] private AttackImplant _upAttackImplant;
        [SerializeField] private AttackImplant _downAttackImplant;
        [SerializeField] private AttackImplant _specialAttackImplant;
        [SerializeField] private ImplantPlaceHolderStash _placeHolderStash;
        
        protected override AttackImplant[] GetImplants()
        {
            AddImplant(_upAttackImplant);
            AddImplant(_downAttackImplant);
            AddImplant(_specialAttackImplant);
            return _attackImplants.ToArray();
        }

        protected override ImplantPlaceHolderStash GetPlaceholderStash()
        {
            return _placeHolderStash;
        }

        protected void AddImplant(AttackImplant attackImplant)
        {
            _attackImplants.Add(attackImplant);
        }
    }
}