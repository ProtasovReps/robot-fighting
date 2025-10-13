using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace FightingSystem.Attacks
{
    public abstract class Attack
    {
        private readonly float _startDelay;
        private readonly float _endDelay;
        
        protected Attack(float duration, float startDelay)
        {
            if (startDelay <= 0)
                throw new ArgumentOutOfRangeException(nameof(startDelay));

            if (duration <= startDelay)
                throw new ArgumentOutOfRangeException(nameof(duration));
            
            _startDelay = startDelay;
            _endDelay = duration - _startDelay;
        }
        
        public async UniTask Launch(CancellationTokenSource tokenSource)
        {
            await UniTask.WaitForSeconds(_startDelay, cancellationToken: tokenSource.Token, cancelImmediately: true);
            await Execute(tokenSource.Token, _endDelay);
        }

        protected abstract UniTask Execute(CancellationToken token, float duration);
    }
}