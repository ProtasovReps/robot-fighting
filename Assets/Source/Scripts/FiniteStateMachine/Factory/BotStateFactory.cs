using FiniteStateMachine.States;

namespace FiniteStateMachine.Factory
{
    public class BotStateFactory : StateFactory
    {
        protected override State[] GetStates()
        {
            return new State[]
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