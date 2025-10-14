using FiniteStateMachine.States;

namespace FiniteStateMachine.Factory
{
    public class BotInputStateFactory : StateFactory
    {
        protected override State[] GetStates()
        {
            return new State[]
            {
                new NothingNearbyState(),
                new WallNearbyState(),
                new OpponentNearbyState(),
                new WallOpponentNearbyState(),
                new ValidAttackDistanceState()
            };
        }
    }
}