using System;
using AnimationSystem;
using HealthSystem;
using Interface;

namespace CharacterSystem
{
    public class Fighter : Character
    {
        private Health _health;

        public IDamageable Health => _health;

        public void Initialize(Health health, CharacterAnimation[] animations)
        {
            if(health == null)
                throw new ArgumentNullException(nameof(health));
            
            _health = health;
            
            Initialize(animations);
        }
    }
}