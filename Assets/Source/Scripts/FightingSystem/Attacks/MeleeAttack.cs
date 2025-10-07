using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using Interface;
using UnityEngine;

namespace FightingSystem.Attacks.Melee
{
    public class MeleeAttack : Attack
    {
        private readonly Spherecaster _spherecaster;

        public MeleeAttack(Damage damage, float delay, float duration, Spherecaster spherecaster)
            : base(damage, duration, delay)
        {
            _spherecaster = spherecaster;
        }

        protected override async UniTask Execute(Damage damage, float duration, CancellationToken token)
        {
            float elapsedTime = 0f;
            bool isHitted = false;
            
            while (elapsedTime < duration)
            {
                if (_spherecaster.TryFindDamageable(out IDamageable<Damage> damageable) && isHitted == false)
                {
                    damageable.AcceptDamage(damage);
                    isHitted = true;
                }

                elapsedTime += Time.deltaTime;
                await UniTask.Yield(cancellationToken: token, cancelImmediately: true);
            }
        }
    }
}