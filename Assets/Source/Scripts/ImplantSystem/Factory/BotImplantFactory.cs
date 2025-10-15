using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem.Factory
{
    public class BotImplantFactory : ImplantFactory
    {
        [SerializeField] private AttackImplant _upAttackImplant;
        [SerializeField] private AttackImplant _downAttackImplant;
        [SerializeField] private AttackImplant _specialAttackImplant;
        [SerializeField] private ImplantPlaceHolderStash _placeHolderStash;
        
        protected override AttackImplant[] GetImplants()
        {
            return new[] { _upAttackImplant, _downAttackImplant, _specialAttackImplant };
        }

        protected override ImplantPlaceHolderStash GetPlaceholderStash()
        {
            return _placeHolderStash;
        }
    }
}