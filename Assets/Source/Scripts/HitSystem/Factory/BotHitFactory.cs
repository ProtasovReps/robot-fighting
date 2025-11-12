using CharacterSystem.Parameters;
using UnityEngine;

namespace HitSystem
{
    public class BotHitFactory : HitFactory
    {
        [SerializeField] private HitColliderStash _colliderStash;
        [SerializeField] private BotParameters _botParameters;
        
        protected override float GetBlockValue()
        {
            return _botParameters.BlockValue;
        }

        protected override HitColliderStash GetColliderStash()
        {
            return _colliderStash;
        }
    }
}