using System;
using Interface;
using R3;

namespace HealthSystem
{
    public class Health : IValueChangeable, IDamageable
    {
        private readonly ReactiveProperty<float> _currentValue;
        
        public Health(float startValue)
        {
            if (startValue <= 0)
                throw new ArgumentOutOfRangeException(nameof(startValue));

            _currentValue = new ReactiveProperty<float>(startValue);
        }
        
        public ReadOnlyReactiveProperty<float> CurrentValue => _currentValue;
        
        public void AcceptDamage(float damage)
        {
            if (damage < 0)
                throw new ArgumentOutOfRangeException(nameof(damage));

            _currentValue.OnNext(_currentValue.Value - damage);
        }
    }
}