using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FiniteStateMachine.States;
using Interface;
using R3;

namespace FightingSystem
{
    public class Block : IContinuous, IDisposable
    {
        private readonly float _blockDuration;
        private readonly IDisposable _subscription;
        // private readonly Collider _collider;
        
        private CancellationTokenSource _tokenSource;
        
        public Block(float blockDuration, IStateMachine stateMachine, IConditionAddable conditionAddable)
        {
            if (blockDuration <= 0)
                throw new ArgumentOutOfRangeException(nameof(blockDuration));
            
            _blockDuration = blockDuration;
            _subscription = stateMachine.Value
                .Where(state => state.Type == typeof(BlockState))
                .Subscribe(_ => Execute().Forget());
            
            conditionAddable.Add<BlockState>(_ => IsContinuing);
        }

        public bool IsContinuing => false; //

        public void Dispose()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
            _subscription.Dispose();
        }
        
        private async UniTaskVoid Execute()
        {
            _tokenSource = new CancellationTokenSource();
            //_collider.enabled = false;
            await UniTask.WaitForSeconds(_blockDuration, cancellationToken: _tokenSource.Token, cancelImmediately: true);
            //_collider.enabled = true;
        }
    }
}