using System;
using HealthSystem;
using Interface;
using UnityEngine;

namespace CharacterSystem
{
    public class Fighter : MonoBehaviour
    {
        public IDamageable Health { get; private set; }
        public IExecutable Stun { get; private set; }
        
        public void Initialize(Health health, IExecutable stun)
        {
            if (health == null)
                throw new ArgumentNullException(nameof(health));

            if (stun == null)
                throw new ArgumentNullException(nameof(stun));
            
            Health = health;
            Stun = stun;
        }
    }
}