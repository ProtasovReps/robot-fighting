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
        public IExecutable Block { get; private set; }
        
        public void Initialize(Health health, IExecutable stun, IExecutable block)
        {
            if (health == null)
                throw new ArgumentNullException(nameof(health));

            if (stun == null)
                throw new ArgumentNullException(nameof(stun));

            if (block == null)
                throw new ArgumentNullException(nameof(block));
            
            Health = health;
            Stun = stun;
            Block = block;
        }
    }
}