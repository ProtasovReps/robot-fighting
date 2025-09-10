using System;
using HitSystem;
using Interface;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightingSystem
{
    public class SuperAttackCharge : IValueChangeable<float>, IDisposable // сделать буст при низком хп
    {
        private const float FullValue = 100f;
        private const float MaxChargeValue = 20f;
        
        private readonly ReactiveProperty<float> _value;
        private readonly Subject<Unit> _fullCharged;
        private readonly IDisposable _subscription;

        public SuperAttackCharge(HitReader hitReader)
        {
            if (hitReader == null)
                throw new ArgumentNullException(nameof(hitReader));

            _value = new ReactiveProperty<float>();
            _fullCharged = new Subject<Unit>();
            
            _subscription = hitReader.Hitted
                .Where(_ => _value.Value < FullValue)
                .Subscribe(_ => Charge());
        }

        public ReadOnlyReactiveProperty<float> Value => _value;
        public Observable<Unit> FullCharged => _fullCharged;
        
        public void Dispose()
        {
            _subscription?.Dispose();
        }

        public void Reset()
        {
            _value.OnNext(0f);
        }
        
        private void Charge()
        {
            _value.OnNext(_value.CurrentValue + Random.Range(0, MaxChargeValue));
            
            if (Value.CurrentValue >= FullValue)
                _fullCharged.OnNext(Unit.Default);         
            
            Debug.LogWarning(Value.CurrentValue);
        }
    }
}