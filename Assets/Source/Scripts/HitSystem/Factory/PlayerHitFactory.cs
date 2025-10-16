using ArmorSystem.Factory;
using HitSystem.FighterParts;
using UnityEngine;

namespace HitSystem
{
    public class PlayerHitFactory : HitFactory // временно такой же как и Bot
    {
        [SerializeField] private ArmorFactory<Torso> _torsoArmor;
        [SerializeField] private ArmorFactory<Legs> _legsArmor;
        [SerializeField] private HitColliderStash _colliderStash;

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