using HitSystem;
using UnityEngine;

namespace EffectSystem.Particle
{
    public class BotHitParticles : HitParticles
    {
        [SerializeField] private HitEffectStash _hitEffectStash;
        
        protected override HitEffectStash GetStash()
        {
            return _hitEffectStash;
        }
    }
}