using HitSystem.FighterParts;
using UnityEngine;
using YG;

namespace HitSystem
{
    public class PlayerHitFactory : HitFactory // временно такой же как и Bot
    {
        [SerializeField] private HitColliderStash _colliderStash;

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