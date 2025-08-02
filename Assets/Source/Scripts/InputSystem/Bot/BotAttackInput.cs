using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using R3;
using Random = UnityEngine.Random;

namespace InputSystem
{
    public class BotAttackInput : IBotInput, IDisposable
    {
        private readonly Subject<Unit>[] _attacks;
        private readonly Subject<Unit> _executed;
        private readonly float _attackInterval;

        private CancellationTokenSource _cancellationTokenSource;

        public BotAttackInput(float attackInterval)
        {
            if (attackInterval <= 0f)
                throw new ArgumentOutOfRangeException(nameof(attackInterval));

            var upAttack = new Subject<Unit>();
            var downAttack = new Subject<Unit>();

            _executed = new Subject<Unit>();
            UpAttack = upAttack;
            DownAttack = downAttack;
            _attackInterval = attackInterval;
            _attacks = new[] { upAttack, downAttack };
        }

        public Observable<Unit> Executed => _executed;
        public Observable<Unit> UpAttack { get; }
        public Observable<Unit> DownAttack { get; }

        public void Dispose()
        {
            _cancellationTokenSource?.Dispose();

            for (int i = 0; i < _attacks.Length; i++)
            {
                _attacks[i].Dispose();
            }

            _executed.Dispose();
        }

        public void Activate()
        {
            RandomizeAttack().Forget();
        }

        public void Deactivate()
        {
            _cancellationTokenSource?.Cancel();
        }

        private async UniTaskVoid RandomizeAttack()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            int attackIndex = Random.Range(0, _attacks.Length);

            _attacks[attackIndex].OnNext(Unit.Default);
            await UniTask.WaitForSeconds(_attackInterval, cancellationToken: _cancellationTokenSource.Token,
                cancelImmediately: true);
            _executed.OnNext(Unit.Default);
        }
    }
}