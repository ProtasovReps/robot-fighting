using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using InputSystem;
using R3;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public class PlayerTransitionFactory : StateTransitionFactory
    {
        [SerializeField] private PlayerInputReader _inputReader;

        protected override void InitializeConditionTransition(
            ConditionBuilder builder, 
            CharacterStateMachine stateMachine)
        {
            builder.Reset<JumpState>(false);
            builder.Reset<AttackState>(false);
            builder.Reset<BlockState>(false);
            
            builder.Add<MoveJumpState>(builder.GetBare<JumpState>());
            builder.Build<MoveJumpState, IdleState>(false);
            
            builder.BuildGlobal<JumpState>(false, typeof(MoveJumpState), typeof(AttackState));
            builder.BuildGlobal<HittedState>(false, typeof(HittedState));
            builder.BuildGlobal<AttackState>(false, typeof(AttackState));
            builder.BuildGlobal<BlockState>(false, typeof(BlockState));

            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, float>(_inputReader.Direction, builder.Get<IdleState>())
                .InitializeTransition<MoveLeftState, float>(_inputReader.Direction, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, float>(_inputReader.Direction, builder.Get<MoveRightState>())
                .InitializeTransition<JumpState, Unit>(_inputReader.JumpPressed, builder.Get<JumpState>())
                .InitializeTransition<MoveJumpState, float>(_inputReader.Direction, builder.Get<MoveJumpState>())
                .InitializeTransition<PunchState, Unit>(_inputReader.PunchPressed, builder.Get<AttackState>())
                .InitializeTransition<KickState, Unit>(_inputReader.KickPressed, builder.Get<AttackState>())
                .InitializeTransition<HittedState, float>(_inputReader.Direction, builder.Get<HittedState>())// не должно быть из Direction
                .InitializeTransition<BlockState, Unit>(_inputReader.BlockPressed, builder.Get<BlockState>());
        }
    }
}