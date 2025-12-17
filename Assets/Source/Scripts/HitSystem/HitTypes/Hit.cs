using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using R3;

namespace HitSystem.HitTypes
{
    public abstract class Hit : IContinuous, IDisposable
    {
        private readonly float _stunDuration;
        private readonly IDisposable _subscription;

        private CancellationTokenSource _source;

        protected Hit(float stunDuration, Observable<Unit> observable)
        {
            if (stunDuration <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(stunDuration));
            }

            _stunDuration = stunDuration;
            _subscription = observable
                .Subscribe(_ => TakeHit());
        }

        public bool IsContinuing { get; private set; }

        public void Dispose()
        {
            _subscription.Dispose();
            _source?.Cancel();
            _source?.Dispose();
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
            _source?.Cancel();
        }

        private async UniTaskVoid Execute()
        {
            _source = new CancellationTokenSource();
            await UniTask.WaitForSeconds(_stunDuration, cancellationToken: _source.Token, cancelImmediately: true);
            IsContinuing = false;
        }
    }
}