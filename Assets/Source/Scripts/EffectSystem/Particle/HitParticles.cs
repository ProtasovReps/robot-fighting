using CharacterSystem.Data;
using HitSystem;
using R3;
using UnityEngine;

namespace EffectSystem.Particle
{
    public class HitParticles : MonoBehaviour
    {
        [SerializeField] private HitReader _hitReader;
        [SerializeField] private FighterData _fighterData;

        private void Awake()
        {
            Subscribe(_hitReader.TorsoHitted, _fighterData.SkinData.HitEffectStash.UpParticleEffect);
            Subscribe(_hitReader.LegsHitted, _fighterData.SkinData.HitEffectStash.DownParticleEffect);
        }

        private void Subscribe(Observable<Unit> observable, ParticleSystem effect)
        {
            observable
                .Subscribe(_ => effect.Play())
                .AddTo(this);
        }
    }
}