using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FiniteStateMachine.States;
using Interface;
using R3;

namespace FightingSystem
{
    public class Hit : IContinuous, IDisposable
    {
        private readonly IDisposable _subscription;
        private readonly float _stunDuration;
        private readonly HitReader _hitReader;
        
        private CancellationTokenSource _cancellationTokenSource;

        public Hit(float stunDuration, HitReader hitReader, IConditionAddable conditionAddable)
        {
            if (stunDuration <= 0)
                throw new ArgumentOutOfRangeException(nameof(stunDuration));

            if (conditionAddable == null)
                throw new ArgumentNullException(nameof(conditionAddable));

            _stunDuration = stunDuration;
            _subscription = hitReader.Hitted
                .Subscribe(_ => TakeHit());
            
            conditionAddable.Add<HittedState>(_ => IsContinuing);
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