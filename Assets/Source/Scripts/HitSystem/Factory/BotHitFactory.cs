using ArmorSystem;
using HitSystem.FighterParts;
using UnityEngine;

namespace HitSystem
{
    public class BotHitFactory : HitFactory
    {
        [SerializeField] private Armor<Torso> _torsoArmor;
        [SerializeField] private Armor<Legs> _legsArmor;
        [SerializeField] private HitColliderStash _colliderStash;

        protected override Armor<Torso> GetTorsoArmor()
        {
            return Instantiate(_torsoArmor);
        }

        protected override Armor<Legs> GetLegsArmor()
        {
            return Instantiate(_legsArmor);
        }

        protected override HitColliderStash GetColliderStash()
        {
            return _colliderStash;
        }
    }
}