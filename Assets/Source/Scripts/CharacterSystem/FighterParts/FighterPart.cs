using System;
using HealthSystem;
using Interface;
using UnityEngine;

namespace CharacterSystem.FighterParts
{
    public abstract class FighterPart : MonoBehaviour, IDamageable
    {
        private Health _health;
        
        public void Initialize(Health health)
        {
            if (health == null)
                throw new ArgumentNullException(nameof(health));

            _health = health;
        }

        public void AcceptDamage(float damage)
        {
            damage = ValidateDamage(damage);
            _health.AcceptDamage(damage);
        }

        protected abstract float ValidateDamage(float damage);
    }
}