using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Random = UnityEngine.Random;

namespace InputSystem.Bot
{
    public class BotAction : IDisposable
    {
        private readonly Action _action;
        private readonly float _duration;
        private readonly Subject<Unit> _executed;
        
        private CancellationTokenSource _tokenSource;
        
        public BotAction(Action action, float duration)
        {
            if (action == null)
                throw new ArgumentNullException(nameof(action));

            if (duration < 0)
                throw new ArgumentOutOfRangeException(nameof(duration));

            _executed = new Subject<Unit>();
            _action = action;
            _duration = duration;
        }

        public Observable<Unit> Executed => _executed;
        
        public void Dispose()
        {
            Deactivate();
            _executed.Dispose();
        }
        
        public void Activate()
        {
            Process().Forget();
        }

        public void Deactivate()
        {
            _tokenSource?.Cancel();
        }

        private async UniTaskVoid Process()
        {
            _tokenSource = new CancellationTokenSource();
            
            _action.Invoke();
            await UniTask.WaitForSeconds(_duration, cancellationToken: _tokenSource.Token, cancelImmediately: true);
            _executed.OnNext(Unit.Default);
        }
    }
}