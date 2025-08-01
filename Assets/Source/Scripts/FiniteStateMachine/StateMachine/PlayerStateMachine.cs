using Interface;

namespace FiniteStateMachine
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerStateMachine(IState[] states) : base(states)
        {
        }
    }
}