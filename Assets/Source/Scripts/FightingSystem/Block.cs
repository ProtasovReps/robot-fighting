using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FightingSystem.AttackDamage;
using FiniteStateMachine.States;
using Interface;
using R3;
using UnityEngine;

namespace FightingSystem
{
    public class Block : IContinuous, IDisposable, IDamageable<Damage>
    {
        private readonly float _blockDuration;
        private readonly float _blockValue;
        private readonly IDisposable _subscription;
        private readonly IDamageable<Damage> _damageable;

        private CancellationTokenSource _source;

        public Block(
            float blockDuration,
            float blockValue,
            IDamageable<Damage> damageable,
            IStateMachine stateMachine,
            IConditionAddable conditionAddable)
        {
            if (blockDuration <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockDuration));
            }

            if (blockValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(blockValue));
            }

            _blockDuration = blockDuration;
            _blockValue = blockValue;
            _damageable = damageable;

            _subscription = stateMachine.Value
                .Where(state => state.Type == typeof(BlockState))
                .Subscribe(_ => Execute().Forget());

            conditionAddable.Add<BlockState>(_ => IsContinuing);
        }

        public bool IsContinuing { get; private set; }

        public void Dispose()
        {
            _source?.Cancel();
            _source?.Dispose();
            _subscription.Dispose();
        }

        public void AcceptDamage(Damage damage)
        {
            if (IsContinuing)
            {
                float reducedDamageValue = Mathf.Clamp(damage.Value - _blockValue, 0, damage.Value);
                damage = new Damage(reducedDamageValue, damage.ImpulseForce);
            }

            _damageable.AcceptDamage(damage);
        }

        private async UniTaskVoid Execute()
        {
            _source = new CancellationTokenSource();

            IsContinuing = true;
            await UniTask.WaitForSeconds(_blockDuration, cancellationToken: _source.Token, cancelImmediately: true);
            IsContinuing = false;
        }
    }
}