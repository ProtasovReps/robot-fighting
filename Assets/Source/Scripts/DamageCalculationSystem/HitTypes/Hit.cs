using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using R3;

namespace DamageCalculationSystem
{
    public abstract class Hit : IContinuous, IDisposable
    {
        private readonly float _stunDuration;
        private readonly IDisposable _subscription;

        private CancellationTokenSource _cancellationTokenSource;

        protected Hit(float stunDuration, Observable<Unit> observable)
        {
            if (stunDuration <= 0)
                throw new ArgumentOutOfRangeException(nameof(stunDuration));

            _stunDuration = stunDuration;
            _subscription = observable
                .Subscribe(_ => TakeHit());
        }

        public bool IsContinuing { get; private set; }

        public void Dispose()
        {
            _subscription.Dispose();
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private void TakeHit()
        {
            Validate();

            IsContinuing = true;
            Execute().Forget();
        }

        private void Validate()
        {
            IsContinuing = false;
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid Execute()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            await UniTask.WaitForSeconds(_stunDuration, cancellationToken: _cancellationTokenSource.Token,
                cancelImmediately: true);
            IsContinuing = false;
        }
    }
}