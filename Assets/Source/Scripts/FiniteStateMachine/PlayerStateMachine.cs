using FiniteStateMachine.States;

namespace FiniteStateMachine
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerStateMachine(State[] states)
            : base(states)
        {
        }
    }
}