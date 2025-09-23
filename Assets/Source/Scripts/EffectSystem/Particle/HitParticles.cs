using HitSystem;
using R3;
using UnityEngine;

namespace EffectSystem.Particle
{
    public class HitParticles : MonoBehaviour
    {
        [SerializeField] private HitReader _hitReader;
        [SerializeField] private ParticleSystem _upEffect;
        [SerializeField] private ParticleSystem _downEffect;

        private void Awake()
        {
            Subscribe(_hitReader.TorsoHitted, _upEffect);
            Subscribe(_hitReader.LegsHitted, _downEffect);
        }

        private void Subscribe(Observable<Unit> observable, ParticleSystem effect)
        {
            observable
                .Subscribe(_ => effect.Play())
                .AddTo(this);
        }
    }
}