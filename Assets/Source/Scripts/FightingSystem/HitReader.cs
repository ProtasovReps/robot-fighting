using Interface;
using R3;
using UnityEngine;

namespace FightingSystem
{
    public class HitReader : MonoBehaviour
    {
        private readonly Subject<Unit> _hitted = new();

        public Observable<Unit> Hitted => _hitted;

        public void Initialize(IValueChangeable<float> health)
        {
            health.Value
                .Pairwise()
                .Where(pair => pair.Current < pair.Previous)
                .Subscribe(_ => _hitted.OnNext(Unit.Default))
                .AddTo(this);
        }
    }
}