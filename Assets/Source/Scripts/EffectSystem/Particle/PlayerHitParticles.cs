using HitSystem;

namespace EffectSystem.Particle
{
    public class PlayerHitParticles : HitParticles
    {
        private HitEffectStash _hitEffectStash;
        
        public void Initialize(HitEffectStash hitEffectStash)
        {
            _hitEffectStash = hitEffectStash;
        }
        
        protected override HitEffectStash GetStash()
        {
            return _hitEffectStash;
        }
    }
}