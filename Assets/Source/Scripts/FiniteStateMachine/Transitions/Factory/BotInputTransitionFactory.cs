using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using R3;

namespace FiniteStateMachine.Transitions.Factory
{
    public class BotInputTransitionFactory : StateTransitionFactory
    {
        protected override void InitializeConditionTransition(ConditionBuilder builder,
            TransitionInitializer initializer)
        {
            builder.Add<WallOpponentNearbyState>(builder.GetBare<WallNearbyState>());
            builder.Merge<WallOpponentNearbyState, OpponentNearbyState>();

            builder.Add<NothingNearbyState>(builder.GetBare<WallNearbyState>());
            builder.Reset<NothingNearbyState>(false);
            builder.Merge<NothingNearbyState, OpponentNearbyState>(false);

            builder.Merge<WallNearbyState, OpponentNearbyState>(false);
            builder.Merge<OpponentNearbyState, WallNearbyState>(false);

            builder.Merge<ValidAttackDistanceState, OpponentNearbyState>(false);
            builder.Merge<ValidAttackDistanceState, WallOpponentNearbyState>(false);
            builder.Merge<NothingNearbyState, ValidAttackDistanceState>(false);
            
            initializer
                .InitializeTransition<WallNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<WallNearbyState>())
                .InitializeTransition<OpponentNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<OpponentNearbyState>())
                .InitializeTransition<NothingNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<NothingNearbyState>())
                .InitializeTransition<WallOpponentNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<WallOpponentNearbyState>())
                .InitializeTransition<ValidAttackDistanceState, Unit>(
                    Observable.EveryUpdate(), builder.Get<ValidAttackDistanceState>());
        }
    }
}