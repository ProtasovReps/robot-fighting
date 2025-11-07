using Interface;
using R3;

namespace UI.Info
{
    public class IntegerView : ObservableInfo
    {
        private IValueChangeable<int> _valueChangeable;
        
        public void Initialize(IValueChangeable<int> valueChangeable)
        {
            _valueChangeable = valueChangeable;
            
            Subscribe(_valueChangeable.Value.Select(_ => Unit.Default));
        }
        
        protected override float GetInfo()
        {
            return _valueChangeable.Value.CurrentValue;
        }
    }
}