using R3;

namespace Interface
{
    public interface IValueChangeable<T>
    {
        ReadOnlyReactiveProperty<T> Value { get; }
    }
}