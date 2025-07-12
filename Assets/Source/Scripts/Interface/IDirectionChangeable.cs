using R3;

namespace Interface
{
    public interface IDirectionChangeable
    {
        ReadOnlyReactiveProperty<float> Direction { get; }
    }
}