using Interface;

namespace FiniteStateMachine
{
    public class BotInputStateMachine : StateMachine
    {
        public BotInputStateMachine(IState[] states) : base(states)
        {
        }
    }
}