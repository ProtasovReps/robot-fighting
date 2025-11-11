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

            Observable<Unit> observable = _valueChangeable.Value.Select(_ => Unit.Default);
            Initialize(observable, () => _valueChangeable.Value.CurrentValue);
        }
    }
}