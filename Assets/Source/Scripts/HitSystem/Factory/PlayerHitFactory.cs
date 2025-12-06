using YG;

namespace HitSystem.Factory
{
    public class PlayerHitFactory : HitFactory
    {
        private HitColliderStash _colliderStash;

        public void Initialize(HitColliderStash hitColliderStash)
        {
            _colliderStash = hitColliderStash;
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