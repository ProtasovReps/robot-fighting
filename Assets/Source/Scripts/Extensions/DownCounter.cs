using System;
using Interface;
using R3;

namespace Extensions
{
    public class DownCounter : IValueChangeable<int>
    {
        private readonly ReactiveProperty<int> _value = new();
        
        public ReadOnlyReactiveProperty<int> Value => _value;
        
        public void AddPoints(int count)
        {
            if (count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            
            int newValue = _value.CurrentValue + count;
            
            SetPoints(newValue);
        }

        public void Reset()
        {
            SetPoints(0);
        }
        
        public void Tick()
        {
            int newValue = _value.CurrentValue;
            
            SetPoints(--newValue);
        }

        private void SetPoints(int newValue)
        {
            _value.OnNext(newValue);
        }
    }
}