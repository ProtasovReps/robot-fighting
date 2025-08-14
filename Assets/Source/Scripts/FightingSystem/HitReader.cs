using CharacterSystem.FighterParts;
using R3;
using UnityEngine;

namespace FightingSystem
{
    public class HitReader : MonoBehaviour
    {
        [SerializeField] private Legs _legs;
        [SerializeField] private Torso _torso;
        [SerializeField] private Subject<Unit> _hitted;

        public void Initialize()
        {
            _hitted = new Subject<Unit>();

            Observable.Merge(_legs.Hitted, _torso.Hitted)
                .Subscribe(_ => _hitted.OnNext(Unit.Default))
                .AddTo(this);
        }

        public Observable<Unit> Hitted => _hitted;
        public Observable<Unit> TorsoHitted => _torso.Hitted;
        public Observable<Unit> LegsHitted => _legs.Hitted;
    }
}