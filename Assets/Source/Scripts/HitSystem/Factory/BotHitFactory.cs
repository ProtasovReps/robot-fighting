using ArmorSystem.Factory;
using CharacterSystem.Parameters;
using HitSystem.FighterParts;
using UnityEngine;

namespace HitSystem
{
    public class BotHitFactory : HitFactory
    {
        [SerializeField] private ArmorFactory<Torso> _torsoArmor;
        [SerializeField] private ArmorFactory<Legs> _legsArmor;
        [SerializeField] private HitColliderStash _colliderStash;
        [SerializeField] private BotParameters _botParameters;
        
        protected override float GetBlockValue()
        {
            return _botParameters.BlockValue;
        }

        protected override ArmorFactory<Torso> GetTorsoArmorFactory()
        {
            return _torsoArmor;
        }

        protected override ArmorFactory<Legs> GetLegsArmorFactory()
        {
            return _legsArmor;
        }

        protected override HitColliderStash GetColliderStash()
        {
            return _colliderStash;
        }
    }
}