using Extensions;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using R3;

namespace FiniteStateMachine.Transitions.Factory
{
    public class BotInputTransitionFactory : StateTransitionFactory<BotInputStateMachine, BotInputConditionBuilder>
    {
        protected override void InitializeConditions(ConditionBuilder builder)
        {
            builder.Add<WallOpponentNearbyState>(builder.GetBare<WallNearbyState>());
            builder.Merge<WallOpponentNearbyState, PlayerNearbyState>();

            builder.Add<NothingNearbyState>(builder.GetBare<WallNearbyState>());
            builder.Reset<NothingNearbyState>(false);
            builder.Merge<NothingNearbyState, PlayerNearbyState>(false);

            builder.Merge<WallNearbyState, PlayerNearbyState>(false);
            builder.Merge<PlayerNearbyState, WallNearbyState>(false);

            builder.Merge<ValidAttackDistanceState, PlayerNearbyState>(false);
            builder.Merge<ValidAttackDistanceState, WallOpponentNearbyState>(false);
            builder.Merge<NothingNearbyState, ValidAttackDistanceState>(false);
        }

        protected override void InstallTransitions(
            StateMachine stateMachine,
            ConditionBuilder builder,
            Disposer disposer)
        {
            var soloInitializer = new TransitionInitializer(new SoloTransitionFactory(), stateMachine)
                .InitializeTransition<WallNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<WallNearbyState>())
                .InitializeTransition<PlayerNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<PlayerNearbyState>())
                .InitializeTransition<NothingNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<NothingNearbyState>())
                .InitializeTransition<WallOpponentNearbyState, Unit>(
                    Observable.EveryUpdate(), builder.Get<WallOpponentNearbyState>())
                .InitializeTransition<ValidAttackDistanceState, Unit>(
                    Observable.EveryUpdate(), builder.Get<ValidAttackDistanceState>());

            disposer.Add(soloInitializer);
        }
    }
}