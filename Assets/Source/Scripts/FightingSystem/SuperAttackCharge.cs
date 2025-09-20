using System;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using HitSystem;
using Interface;
using R3;
using Random = UnityEngine.Random;

namespace FightingSystem
{
    public class SuperAttackCharge : IFloatValueChangeable, IDisposable
    {
        private const float FullValue = 100f;
        private const float MaxChargeValue = 40;
        private const float MinChargeValue = 20;

        private readonly ReactiveProperty<float> _value;
        private readonly CompositeDisposable _subscriptions;

        public SuperAttackCharge(
            HitReader hitReader,
            IStateMachine stateMachine,
            PlayerConditionBuilder conditionBuilder)
        {
            if (hitReader == null)
                throw new ArgumentNullException(nameof(hitReader));

            MaxValue = FullValue;

            _subscriptions = new CompositeDisposable();
            _value = new ReactiveProperty<float>();

            hitReader.Hitted
                .Where(_ => _value.Value < FullValue)
                .Subscribe(_ => Charge())
                .AddTo(_subscriptions);

            stateMachine.Value
                .Where(state => state.Type == typeof(SuperAttackState))
                .Subscribe(_ => Reset())
                .AddTo(_subscriptions);
            
            conditionBuilder.Add<SuperAttackState>(_ => _value.CurrentValue >= FullValue);
        }

        public ReadOnlyReactiveProperty<float> Value => _value;
        public float MaxValue { get; }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        public void Reset()
        {
            _value.OnNext(0f);
        }

        private void Charge()
        {
            _value.OnNext(_value.CurrentValue + Random.Range(MinChargeValue, MaxChargeValue));
        }
    }
}