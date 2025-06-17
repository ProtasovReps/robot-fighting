using System;
using HealthSystem;
using Interface;
using UnityEngine;

namespace CharacterSystem
{
    public class Fighter : MonoBehaviour
    {
        private Health _health;

        public IDamageable Health => _health;

        public void Initialize(Health health)
        {
            if(health == null)
                throw new ArgumentNullException(nameof(health));
            
            _health = health;
        }
    }
}