using HitSystem;
using R3;
using UnityEngine;

namespace EffectSystem.Particle
{
    public abstract class HitParticles : MonoBehaviour
    {
        [SerializeField] private HitReader _hitReader;

        private void Awake()
        {
            HitEffectStash stash = GetStash();
            
            Subscribe(_hitReader.TorsoHitted, stash.UpParticleEffect);
            Subscribe(_hitReader.LegsHitted, stash.DownParticleEffect);
        }

        private void Subscribe(Observable<Unit> observable, ParticleSystem effect)
        {
            observable
                .Subscribe(_ => effect.Play())
                .AddTo(this);
        }

        protected abstract HitEffectStash GetStash();
    }
}