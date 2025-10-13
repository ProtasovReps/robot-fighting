using System.Threading;
using Cysharp.Threading.Tasks;
using Extensions;
using Interface;
using UnityEngine;

namespace FightingSystem.Attacks
{
    public class MeleeAttack : Attack
    {
        private readonly Spherecaster _spherecaster;
        private readonly Damage _damage;

        public MeleeAttack(float duration, float delay, Damage damage, Spherecaster spherecaster)
            : base(duration, delay)
        {
            _damage = damage;
            _spherecaster = spherecaster;
        }

        protected override async UniTask Execute(CancellationToken token, float duration)
        {
            float elapsedTime = 0f;
            bool isHitted = false;

            while (elapsedTime < duration)
            {
                if (_spherecaster.TryFindDamageable(out IDamageable<Damage> damageable) && isHitted == false)
                {
                    damageable.AcceptDamage(_damage);
                    isHitted = true;
                }

                elapsedTime += Time.deltaTime;
                await UniTask.Yield(cancellationToken: token, cancelImmediately: true);
            }
        }
    }
}