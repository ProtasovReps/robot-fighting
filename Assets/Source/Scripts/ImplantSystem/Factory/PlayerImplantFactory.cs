using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem.Factory
{
    public class PlayerImplantFactory : ImplantFactory // тут все получать из сейва в будущем
    {
        [SerializeField] private AttackImplant _upAttackImplant;
        [SerializeField] private AttackImplant _downAttackImplant;
        [SerializeField] private AttackImplant _superAttackImplant;
        [SerializeField] private ImplantPlaceHolderStash _placeHolderStash;
        
        protected override AttackImplant[] GetImplants()
        {
            return new[] { _upAttackImplant, _downAttackImplant, _superAttackImplant };
        }

        protected override ImplantPlaceHolderStash GetPlaceholderStash()
        {
            return _placeHolderStash;
        }
    }
}