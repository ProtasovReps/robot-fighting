using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Random = UnityEngine.Random;

namespace InputSystem
{
    public class BotAttackInput : IDisposable
    {
        private readonly Subject<Unit>[] _attacks;
        private readonly float _attackDelay;

        private CancellationTokenSource _cancellationTokenSource;

        public BotAttackInput(float attackDelay)
        {
            if (attackDelay <= 0f)
                throw new ArgumentOutOfRangeException(nameof(attackDelay));

            var upAttack = new Subject<Unit>();
            var downAttack = new Subject<Unit>();

            UpAttack = upAttack;
            DownAttack = downAttack;
            _attackDelay = attackDelay;
            _attacks = new[] { upAttack, downAttack };

            RandomizeAttack().Forget();
        }

        public Subject<Unit> UpAttack { get; }
        public Subject<Unit> DownAttack { get; }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            for (int i = 0; i < _attacks.Length; i++)
            {
                _attacks[i].Dispose();
            }
        }

        private async UniTaskVoid RandomizeAttack()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            while (_cancellationTokenSource.IsCancellationRequested == false)
            {
                int attackIndex = Random.Range(0, _attacks.Length);

                _attacks[attackIndex].OnNext(Unit.Default);
                await UniTask.WaitForSeconds(_attackDelay, cancellationToken: _cancellationTokenSource.Token,
                    cancelImmediately: true);
            }
        }
    }
}