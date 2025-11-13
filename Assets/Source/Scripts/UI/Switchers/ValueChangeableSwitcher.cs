using Interface;
using R3;
using Reflex.Attributes;

namespace UI.Switchers
{
    public class ValueChangeableSwitcher<T> : ButtonSwitcher
    where T : IFloatValueChangeable
    {
        private T _valueChangeable;

        [Inject]
        private void Inject(T valueChangeable)
        {
            _valueChangeable = valueChangeable;
        }
        
        private void Awake()
        {
            _valueChangeable.Value
                .Where(_ => IsFilled())
                .Subscribe(_ => Enable())
                .AddTo(this);
            
            _valueChangeable.Value
                .Where(_ => IsFilled() == false)
                .Subscribe(_ => Disable())
                .AddTo(this);
        }

        private bool IsFilled()
        {
            return _valueChangeable.Value.CurrentValue >= _valueChangeable.MaxValue;
        }
    }
}