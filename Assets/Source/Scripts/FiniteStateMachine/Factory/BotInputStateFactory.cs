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
                new NothingNearbyState(),
                new WallNearbyState(),
                new OpponentNearbyState(),
                new WallOpponentNearbyState()
            };
        }
    }
}