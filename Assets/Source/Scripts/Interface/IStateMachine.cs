using R3;

namespace Interface
{
    public interface IStateMachine
    {
        ReadOnlyReactiveProperty<IState> CurrentState { get; }
    }
}