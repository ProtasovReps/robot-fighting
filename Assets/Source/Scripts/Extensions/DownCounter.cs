using Interface;
using R3;

namespace Extensions
{
    public class DownCounter : IValueChangeable<int>
    {
        private readonly ReactiveProperty<int> _value;

        public DownCounter(int startValue)
        {
            _value = new ReactiveProperty<int>();
            
            SetSkillPoints(startValue);
        }
        
        public ReadOnlyReactiveProperty<int> Value => _value;
        
        public void Tick()
        {
            int newValue = _value.CurrentValue;
            
            SetSkillPoints(--newValue);
        }

        private void SetSkillPoints(int newValue)
        {
            _value.OnNext(newValue);
        }
    }
}