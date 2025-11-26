using System;
using Interface;
using R3;

namespace Extensions
{
    public class DownCounter : IValueChangeable<int>
    {
        private readonly ReactiveProperty<int> _value = new();
        private readonly Subject<Unit> _ended = new();
        
        public ReadOnlyReactiveProperty<int> Value => _value;
        public Observable<Unit> Ended => _ended;
        
        public void AddPoints(int count)
        {
            if (count <= 0)
                throw new ArgumentOutOfRangeException(nameof(count));
            
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
            
            if(_value.CurrentValue <= 0)
                _ended.OnNext(Unit.Default);
        }

        private void SetPoints(int newValue)
        {
            _value.OnNext(newValue);
        }
    }
}