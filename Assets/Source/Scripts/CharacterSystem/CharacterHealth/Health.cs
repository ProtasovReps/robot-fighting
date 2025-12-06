using System;
using Interface;
using R3;
using UnityEngine;

namespace CharacterSystem.CharacterHealth
{
    public class Health : IFloatValueChangeable, IDamageable<float>
    {
        private readonly ReactiveProperty<float> _value;
        
        protected Health(float startValue)
        {
            if (startValue <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startValue));
            }

            MaxValue = startValue;
            _value = new ReactiveProperty<float>(MaxValue);
        }
        
        public ReadOnlyReactiveProperty<float> Value => _value;
        public float MaxValue { get; }
        
        public void AcceptDamage(float damage)
        {
            float clampedDamage = Mathf.Clamp(damage, 0, _value.CurrentValue);
            
            _value.OnNext(_value.Value - clampedDamage);
        }
    }
}