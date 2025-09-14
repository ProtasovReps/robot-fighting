using System;
using Interface;
using R3;
using UnityEngine;

namespace HealthSystem
{
    public class Health : IValueChangeable<float>, IDamageable<float>
    {
        private readonly ReactiveProperty<float> _value;
        
        protected Health(float startValue)
        {
            if (startValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(startValue));

            _value = new ReactiveProperty<float>(startValue);
        }
        
        public ReadOnlyReactiveProperty<float> Value => _value;
        
        public void AcceptDamage(float damage)
        {
            float clampedDamage = Mathf.Clamp(damage, 0, _value.CurrentValue);
            
            _value.OnNext(_value.Value - clampedDamage);
        }
    }
}