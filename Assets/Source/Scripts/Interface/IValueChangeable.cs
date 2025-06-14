using R3;

namespace Interface
{
    public interface IValueChangeable
    {
        ReadOnlyReactiveProperty<float> CurrentValue { get; }
    }
}