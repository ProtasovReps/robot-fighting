using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FiniteStateMachine.States;
using Interface;
using R3;

namespace CharacterSystem
{
    public class Stretch : IContinuous, IDisposable
    {
        private const float StretchDelay = 3f;

        private readonly CompositeDisposable _subscriptions;

        private CancellationTokenSource _cancellationTokenSource;

        public Stretch(IStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            Type idleType = typeof(IdleState);

            _subscriptions = new CompositeDisposable();

            conditionAddable.Add<StretchState>(_ => IsContinuing);

            stateMachine.Value
                .Where(value => value.Type == idleType)
                .Subscribe(_ => StretchDelayed().Forget())
                .AddTo(_subscriptions);

            stateMachine.Value
                .Where(state => state.Type != idleType && state.Type != typeof(StretchState))
                .Subscribe(_ => Reset())
                .AddTo(_subscriptions);
        }

        public bool IsContinuing { get; private set; }

        public void Dispose()
        {
            _subscriptions?.Dispose();
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid StretchDelayed()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            await UniTask.WaitForSeconds(
                StretchDelay,
                cancellationToken: _cancellationTokenSource.Token,
                cancelImmediately: true);
            
            IsContinuing = true;
        }

        private void Reset()
        {
            _cancellationTokenSource?.Cancel();
            IsContinuing = false;
        }
    }
}