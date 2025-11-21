using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FightingSystem.Guns;

namespace FightingSystem.Attacks
{
    public class RangedAttack : Attack
    {
        private readonly Shooter _shooter;
        
        public RangedAttack(float duration, float startDelay, float endDelay, Shooter shooter)
            : base(duration, startDelay, endDelay)
        {
            if (shooter == null)
                throw new ArgumentNullException(nameof(shooter));

            _shooter = shooter;
        }

        protected override async UniTask Execute(CancellationToken token, float duration)
        {
            _shooter.Shoot();
            await UniTask.WaitForSeconds(duration, cancellationToken: token, cancelImmediately: true);
        }
    }
}