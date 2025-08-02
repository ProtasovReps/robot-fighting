using System;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using FiniteStateMachine.States;
using Interface;
using R3;
using Random = UnityEngine.Random;

namespace InputSystem
{
    public class BotMovementInput : IBotInput, IDisposable
    {
        private readonly float _directionChangeInterval;
        private readonly int[] _directions;
        private readonly IStateMachine _stateMachine;

        private Subject<Unit> _executed;
        private CancellationTokenSource _cancellationTokenSource;

        public BotMovementInput(float directionChangeInterval, IStateMachine stateMachine)
        {
            if (directionChangeInterval <= 0f)
                throw new ArgumentOutOfRangeException(nameof(directionChangeInterval));

            if (stateMachine == null)
                throw new ArgumentNullException(nameof(stateMachine));

            _directionChangeInterval = directionChangeInterval;
            _stateMachine = stateMachine;
            _directions = new[] { -1, 0, 1, 1 };
            _executed = new Subject<Unit>();
        }

        public Observable<Unit> Executed => _executed;
        public float Direction { get; private set; }

        public void Dispose()
        {
            Deactivate();
            _cancellationTokenSource?.Dispose();
        }

        public void Activate()
        {
            Move().Forget();
        }

        public void Deactivate()
        {
            _cancellationTokenSource?.Cancel();
            Direction = 0;
        }

        private async UniTaskVoid Move()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            ChangeDirection();
            await UniTask.WaitForSeconds(_directionChangeInterval, cancellationToken: _cancellationTokenSource.Token,
                cancelImmediately: true);
            _executed.OnNext(Unit.Default);
        }

        private void ChangeDirection()
        {
            int newDirection;
            Type currentState = _stateMachine.CurrentState.CurrentValue.Type;

            if (currentState == typeof(OpponentNearbyState))
                newDirection = _directions.Min();
            else if (currentState == typeof(WallNearbyState))
                newDirection = _directions.Max();
            else
                newDirection = _directions[Random.Range(0, _directions.Length)];

            Direction = newDirection;
        }
    }
}