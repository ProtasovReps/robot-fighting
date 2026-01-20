using FiniteStateMachine.States;

namespace FiniteStateMachine
{
    public class BotStateMachine : StateMachine
    {
        public BotStateMachine(State[] states)
            : base(states)
        {
        }
    }
}