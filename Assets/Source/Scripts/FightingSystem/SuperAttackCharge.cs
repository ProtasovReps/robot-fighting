using System;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using HitSystem;
using Interface;
using R3;
using UnityEngine;

namespace FightingSystem
{
    public class SuperAttackCharge : IFloatValueChangeable, IDisposable
    {
        private const float FullValue = 100f;
        private const float MaxChargeValue = 30;

        private readonly ReactiveProperty<float> _value;
        private readonly IDisposable _subscription;

        public SuperAttackCharge(HitReader hitReader, PlayerConditionBuilder conditionBuilder)
        {
            if (hitReader == null)
                throw new ArgumentNullException(nameof(hitReader));

            MaxValue = FullValue;
            
            _value = new ReactiveProperty<float>();
            _subscription = hitReader.Hitted
                .Where(_ => _value.Value < FullValue)
                .Subscribe(_ => Charge());

            conditionBuilder.Add<SuperAttackState>(_ => _value.CurrentValue >= FullValue);
        }

        public ReadOnlyReactiveProperty<float> Value => _value;
        public float MaxValue { get; }

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