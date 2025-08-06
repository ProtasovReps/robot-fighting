using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using InputSystem;
using InputSystem.Bot;
using R3;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public class BotTransitionFactory : StateTransitionFactory
    {
        [SerializeField] private BotInputReader _botInputReader;
        
        protected override void InitializeConditionTransition(ConditionBuilder builder,
            StateMachine stateMachine)
        {
            builder.Reset<AttackState>(false);
            builder.BuildGlobal<HittedState>(false);
            builder.BuildGlobal<AttackState>(false, typeof(HittedState));
            
            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, float>(_botInputReader.Direction, builder.Get<IdleState>())
                .InitializeTransition<MoveLeftState, float>(_botInputReader.Direction, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, float>(_botInputReader.Direction, builder.Get<MoveRightState>())
                .InitializeTransition<HittedState, float>(_botInputReader.Direction, builder.Get<HittedState>()) // Не должно из InputReader
                .InitializeTransition<PunchState, Unit>(_botInputReader.UpAttackPressed, builder.Get<AttackState>())
                .InitializeTransition<KickState, Unit>(_botInputReader.DownAttackPressed, builder.Get<AttackState>());
        }
    }
}