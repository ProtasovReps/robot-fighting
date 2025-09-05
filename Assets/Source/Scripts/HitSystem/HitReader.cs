using CharacterSystem.FighterParts;
using FightingSystem;
using R3;
using UnityEngine;

namespace HitSystem
{
    public class HitReader : MonoBehaviour
    {
        private Subject<Damage> _hitted;
        private Legs _legs;
        private Torso _torso;

        public void Initialize(Torso torso, Legs legs)
        {
            _hitted = new Subject<Damage>();
            _torso = torso;
            _legs = legs;

            Observable.Merge(_torso.Hitted, _legs.Hitted)
                .Subscribe(damage => _hitted.OnNext(damage))
                .AddTo(this);
        }

        public Observable<Damage> Hitted => _hitted;
        public Observable<Unit> TorsoHitted => _torso.Hitted.Select(_ => Unit.Default);
        public Observable<Unit> LegsHitted => _legs.Hitted.Select(_ => Unit.Default);
    }
}