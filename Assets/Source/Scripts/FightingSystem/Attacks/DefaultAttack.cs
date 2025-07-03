using System;
using Interface;

namespace FightingSystem.Attacks
{
    public class DefaultAttack : IAttack
    {
        private readonly float _damage;

        public DefaultAttack(float damage, float delay, Type requiredState)
        {
            _damage = damage;
            Delay = delay;
            RequiredState = requiredState;
        }

        public float Delay { get; } 
        public Type RequiredState { get; }

        public void ApplyDamage(IDamageable damageable)
        {
            damageable.AcceptDamage(_damage);
        }
    }
}