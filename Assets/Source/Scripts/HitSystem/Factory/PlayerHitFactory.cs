using Extensions;
using Interface;
using UnityEngine;
using YG;

namespace HitSystem.Factory
{
    public class PlayerHitFactory : HitFactory
    {
        [SerializeField] private ColliderSwitcher _colliderSwitcher;
        
        private HitColliderStash _colliderStash;

        public void Initialize(HitColliderStash hitColliderStash, IStateMachine machine)
        {
            _colliderStash = hitColliderStash;
            _colliderSwitcher.Initialize(machine, hitColliderStash);
        }
        
        protected override float GetBlockValue()
        {
            return YG2.saves.BlockStat;
        }

        protected override HitColliderStash GetColliderStash()
        {
            return _colliderStash;
        }
    }
}