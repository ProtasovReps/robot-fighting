using System;
using HealthSystem;
using Interface;
using R3;
using UnityEngine;

namespace CharacterSystem.FighterParts
{
    public abstract class FighterPart : MonoBehaviour, IDamageable
    {
        private Health _health;
        private Subject<Unit> _hitted;
        
        public Observable<Unit> Hitted => _hitted;
        
        public void Initialize(Health health)
        {
            if (health == null)
                throw new ArgumentNullException(nameof(health));

            _health = health;
            _hitted = new Subject<Unit>();
        }

        public void AcceptDamage(float damage)
        {
            damage = ValidateDamage(damage);
            _health.AcceptDamage(damage);
            _hitted.OnNext(Unit.Default);
        }

        protected abstract float ValidateDamage(float damage);
    }
}