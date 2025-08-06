using FiniteStateMachine.States;
using R3;

namespace Interface
{
    public interface IStateMachine
    {
        ReadOnlyReactiveProperty<State> CurrentState { get; }
    }
}