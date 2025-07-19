using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using R3;

namespace FightingSystem
{
    public class Stun : IExecutable, IDisposable
    {
        private readonly IDisposable _subscription;
        private readonly Subject<Unit> _isStunned;
        private readonly float _stunDuration;

        private CancellationTokenSource _cancellationTokenSource; 
        
        public Stun(float stunDuration, IValueChangeable valueChangeable)
        {
            if (stunDuration <= 0)
                throw new ArgumentOutOfRangeException(nameof(stunDuration));

            if (valueChangeable == null)
                throw new ArgumentNullException(nameof(valueChangeable));

            _stunDuration = stunDuration;
            _subscription = valueChangeable.CurrentValue
                .Pairwise()
                .Where(pair => pair.Current < pair.Previous)
                .Subscribe(_ => Validate());
        }

        public bool IsExecuting { get; private set; }

        public void Dispose()
        {
            _subscription.Dispose();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private void Validate()
        {
            if (IsExecuting)
                _cancellationTokenSource.Cancel();

            IsExecuting = true;
            Execute().Forget();
        }

        private async UniTaskVoid Execute()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await UniTask.WaitForSeconds(_stunDuration, cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
            IsExecuting = false;
        }
    }
}