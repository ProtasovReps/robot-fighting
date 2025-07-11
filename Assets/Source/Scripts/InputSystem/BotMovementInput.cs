using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Random = UnityEngine.Random;

namespace InputSystem
{
    public class BotMovementInput : IDisposable
    {
        private readonly float _moveTime;
        private readonly int[] _directions;

        private CancellationTokenSource _cancellationTokenSource;

        public BotMovementInput(float moveTime)
        {
            if (moveTime <= 0f)
                throw new ArgumentOutOfRangeException(nameof(moveTime));

            _moveTime = moveTime;
            _directions = new[] { -1, 0, 1 };

            RandomizeDirection().Forget();
        }

        public float Direction { get; private set; }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private async UniTaskVoid RandomizeDirection()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                int direction = Random.Range(0, _directions.Length);
                await SetDirection(_directions[direction], _cancellationTokenSource.Token);
            }
        }

        private async UniTask SetDirection(int direction, CancellationToken cancellationToken)
        {
            Direction = direction;
            await UniTask.WaitForSeconds(_moveTime, cancellationToken: cancellationToken, cancelImmediately: true);
            Direction = 0;
        }
    }
}