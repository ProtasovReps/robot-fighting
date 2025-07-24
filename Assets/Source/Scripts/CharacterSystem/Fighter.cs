using System;
using HealthSystem;
using Interface;
using UnityEngine;

namespace CharacterSystem
{
    public class Fighter : MonoBehaviour
    {
        public IDamageable Health { get; private set; }
        
        public void Initialize(Health health)
        {
            if (health == null)
                throw new ArgumentNullException(nameof(health));

            Health = health;
        }
    }
}