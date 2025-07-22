using FiniteStateMachine.States;
using Interface;

namespace FiniteStateMachine.Factory
{
    public class BotStateMachineFactory : StateMachineFactory
    {
        protected override IState[] GetStates()
        {
            return new IState[]
            {
                new IdleState(),
                new MoveLeftState(),
                new MoveRightState(),
                new HittedState(),
                new PunchState(),
                new KickState()
            };
        }
       
    }
}