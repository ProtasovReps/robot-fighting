using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FightingSystem.Attacks
{
    public abstract class Attack
    {
        private readonly Damage _damage;
        private readonly float _startDelay;
        private readonly float _endDelay;
        
        protected Attack(Damage damage, float duration, float startDelay)
        {
            if (startDelay <= 0)
                throw new ArgumentOutOfRangeException(nameof(startDelay));

            if (duration <= startDelay)
                throw new ArgumentOutOfRangeException(nameof(duration));
            
            _damage = damage;
            _startDelay = startDelay;
            _endDelay = duration - _startDelay;
        }

        public async UniTask Launch(CancellationTokenSource tokenSource)
        {
            await UniTask.WaitForSeconds(_startDelay, cancellationToken: tokenSource.Token, cancelImmediately: true);
            Debug.LogError("biu");
            Execute(_damage);
            await UniTask.WaitForSeconds(_endDelay, cancellationToken: tokenSource.Token, cancelImmediately: true);
        }

        protected abstract void Execute(Damage damage);
    }
}