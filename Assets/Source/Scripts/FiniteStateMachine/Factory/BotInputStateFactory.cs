using FiniteStateMachine.States;
using Interface;

namespace FiniteStateMachine.Factory
{
    public class BotInputStateFactory : StateFactory
    {
        protected override IState[] GetStates()
        {
            return new IState[]
            {
                new PlayerNearbyState()
            };
        }
    }
}