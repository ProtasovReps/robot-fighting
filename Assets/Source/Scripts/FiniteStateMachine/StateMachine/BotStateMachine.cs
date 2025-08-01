using Interface;

namespace FiniteStateMachine
{
    public class BotStateMachine : StateMachine
    {
        public BotStateMachine(IState[] states) : base(states)
        {
        }
    }
}