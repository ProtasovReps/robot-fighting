using R3;

namespace Interface
{
    public interface IStateChangeable
    {
        ReadOnlyReactiveProperty<IState> CurrentState { get; }
    }
}