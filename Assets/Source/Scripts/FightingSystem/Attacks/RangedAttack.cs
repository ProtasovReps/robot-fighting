using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using FightingSystem.Guns;

namespace FightingSystem.Attacks
{
    public class RangedAttack : Attack
    {
        private readonly Gun _gun;
        
        public RangedAttack(float duration, float startDelay, Gun gun)
            : base(duration, startDelay)
        {
            if (gun == null)
                throw new ArgumentNullException(nameof(gun));

            _gun = gun;
        }

        protected override async UniTask Execute(CancellationToken token, float duration)
        {
            _gun.Shoot();
            await UniTask.WaitForSeconds(duration, cancellationToken: token, cancelImmediately: true);
        }
    }
}