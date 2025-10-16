using HitSystem;
using UnityEngine;

namespace EffectSystem.Particle
{
    public class PlayerHitParticles : HitParticles // пока без сейвов дублирует логику bot'a
    {
        [SerializeField] private HitEffectStash _hitEffectStash;
        
        protected override HitEffectStash GetStash()
        {
            return _hitEffectStash;
        }
    }
}