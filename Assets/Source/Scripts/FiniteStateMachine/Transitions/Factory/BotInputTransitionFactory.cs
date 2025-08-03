using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using R3;

namespace FiniteStateMachine.Transitions.Factory
{
    public class BotInputTransitionFactory : StateTransitionFactory
    {
        protected override void InitializeConditionTransition(ConditionBuilder builder,
            StateMachine stateMachine)
        {
            builder.Add<WallOpponentNearbyState>(builder.GetBare<WallNearbyState>());
            builder.Build<WallOpponentNearbyState, OpponentNearbyState>();

            builder.Add<NothingNearbyState>(builder.GetBare<WallNearbyState>());
            builder.Reset<NothingNearbyState>(false);
            builder.Build<NothingNearbyState, OpponentNearbyState>(false);

            builder.Build<WallNearbyState, OpponentNearbyState>(false);
            builder.Build<OpponentNearbyState, WallNearbyState>(false);
            
            new TransitionInitializer(stateMachine)
                .InitializeTransition<WallNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<WallNearbyState>())
                .InitializeTransition<OpponentNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<OpponentNearbyState>())
                .InitializeTransition<NothingNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<NothingNearbyState>())
                .InitializeTransition<WallOpponentNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<WallOpponentNearbyState>());
        }
    }
}