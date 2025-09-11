using System;
using FiniteStateMachine.States;
using HitSystem;
using Interface;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FightingSystem
{
    public class SuperAttackCharge<T> : IValueChangeable<float>, IDisposable
        where T : IConditionAddable
    {
        private const float FullValue = 100f;
        private const float MaxChargeValue = 30;

        private readonly ReactiveProperty<float> _value;
        private readonly IDisposable _subscription;

        public SuperAttackCharge(HitReader hitReader, T conditionAddable)
        {
            if (hitReader == null)
                throw new ArgumentNullException(nameof(hitReader));

            _value = new ReactiveProperty<float>();
            _subscription = hitReader.Hitted
                .Where(_ => _value.Value < FullValue)
                .Subscribe(_ => Charge());

            conditionAddable.Add<SuperAttackState>(_ => _value.CurrentValue >= FullValue);
        }

        public ReadOnlyReactiveProperty<float> Value => _value;

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        public void Reset()
        {
            _value.OnNext(0f);
        }

        private void Charge() // clamp
        {
            // _value.OnNext(_value.CurrentValue + Random.Range(0, MaxChargeValue));
            _value.OnNext(_value.CurrentValue + MaxChargeValue);
            
            Debug.LogWarning(Value.CurrentValue);
        }
    }
}