using CharacterSystem.FighterParts;
using R3;
using UnityEngine;

namespace DamageCalculationSystem
{
    public class HitReader : MonoBehaviour
    {
        private Subject<float> _hitted;
        private Legs _legs;
        private Torso _torso;

        public void Initialize(Torso torso, Legs legs)
        {
            _hitted = new Subject<float>();
            _torso = torso;
            _legs = legs;

            Observable.Merge(_torso.Hitted, _legs.Hitted)
                .Subscribe(damage => _hitted.OnNext(damage))
                .AddTo(this);
        }

        public Observable<float> Hitted => _hitted;
        public Observable<Unit> TorsoHitted => _torso.Hitted.Select(_ => Unit.Default);
        public Observable<Unit> LegsHitted => _legs.Hitted.Select(_ => Unit.Default);
    }
}