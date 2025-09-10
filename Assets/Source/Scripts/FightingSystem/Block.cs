using System;
using System.Threading;
using Cysharp.Threading.Tasks;
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

        private CancellationTokenSource _tokenSource;

        public Block(
            float blockDuration,
            float blockValue,
            IDamageable<Damage> damageable,
            IStateMachine stateMachine,
            IConditionAddable conditionAddable)
        {
            if (blockDuration <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockDuration));

            if (blockValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockValue));

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
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _subscription.Dispose();
        }

        public void AcceptDamage(Damage damage)
        {
            if (IsContinuing)
            {
                float reducedDamageValue = Mathf.Clamp(damage.Value - _blockValue, 0, damage.Value);
                damage = new Damage(reducedDamageValue, damage.ImpulseForce, damage.Type);
            }

            _damageable.AcceptDamage(damage);
        }

        private async UniTaskVoid Execute()
        {
            _tokenSource = new CancellationTokenSource();

            IsContinuing = true;
            await UniTask.WaitForSeconds(_blockDuration, cancellationToken: _tokenSource.Token,
                cancelImmediately: true);
            IsContinuing = false;
        }
    }
}