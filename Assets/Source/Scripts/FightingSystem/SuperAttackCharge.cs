using System;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using HitSystem;
using Interface;
using R3;
using YG;
using Random = UnityEngine.Random;

namespace FightingSystem
{
    public class SuperAttackCharge : IFloatValueChangeable, IDisposable
    {
        private const int GuideIndex = 5;
        private const float FullValue = 100f;
        private const float MaxChargeValue = 80;
        private const float MinChargeValue = 40;

        private readonly ReactiveProperty<float> _value;
        private readonly CompositeDisposable _subscriptions;

        public SuperAttackCharge(
            HitReader hitReader,
            IStateMachine stateMachine,
            PlayerConditionBuilder conditionBuilder)
        {
            if (hitReader == null)
            {
                throw new ArgumentNullException(nameof(hitReader));
            }

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

            if (YG2.saves.SceneIndex < GuideIndex)
            {
                conditionBuilder.Add<SuperAttackState>(_ => true);
            }
            else
            {
                conditionBuilder.Add<SuperAttackState>(_ => _value.CurrentValue >= FullValue);
            }
        }

        public ReadOnlyReactiveProperty<float> Value => _value;
        public float MaxValue { get; }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        private void Reset()
        {
            _value.OnNext(0f);
        }

        private void Charge()
        {
            _value.OnNext(_value.CurrentValue + Random.Range(MinChargeValue, MaxChargeValue));
        }
    }
}