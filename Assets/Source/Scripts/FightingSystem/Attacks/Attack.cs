using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace FightingSystem.Attacks
{
    public abstract class Attack
    {
        private readonly float _duration;
        private readonly float _startDelay;
        private readonly float _endDelay;
        
        protected Attack(float duration, float startDelay, float endDelay)
        {
            if (startDelay <= 0)
                throw new ArgumentOutOfRangeException(nameof(startDelay));

            _duration = duration;
            _startDelay = startDelay;
            _endDelay = endDelay;
        }

        public async UniTask Launch(CancellationTokenSource tokenSource)
        {
            Debug.Log("Жду");
            await UniTask.WaitForSeconds(_startDelay, cancellationToken: tokenSource.Token, cancelImmediately: true);
            await Execute(tokenSource.Token, _duration);
            Debug.Log("Дожидаю");
            await UniTask.WaitForSeconds(_endDelay, cancellationToken: tokenSource.Token, cancelImmediately: true);
        }

        protected abstract UniTask Execute(CancellationToken token, float duration);
    }
}