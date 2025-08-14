using FightingSystem;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using InputSystem;
using R3;
using Reflex.Attributes;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public class PlayerTransitionFactory : StateTransitionFactory
    {
        [SerializeField] private HitReader _hitReader;

        private PlayerMoveInputReader _moveInput;
        private PlayerAttackInputReader _attackInputReader;
        
        [Inject]
        private void Inject(PlayerMoveInputReader moveInput, PlayerAttackInputReader attackInputReader)
        {
            _moveInput = moveInput;
            _attackInputReader = attackInputReader;
        }
        
        protected override void InitializeConditionTransition(
            ConditionBuilder builder, 
            StateMachine stateMachine)
        {
            builder.Reset<JumpState>(false);
            builder.Reset<AttackState>(false);
            builder.Reset<BlockState>(false);
            
            builder.Add<MoveJumpState>(builder.GetBare<JumpState>());
            builder.Build<MoveJumpState, IdleState>(false);
            
            builder.BuildGlobal<JumpState>(false, typeof(MoveJumpState), typeof(AttackState));
            builder.BuildGlobal<UpHittedState>(false, typeof(DownHittedState));
            builder.BuildGlobal<DownHittedState>(false, typeof(UpHittedState));
            builder.BuildGlobal<AttackState>(false, typeof(UpHittedState), typeof(DownHittedState));
            builder.BuildGlobal<BlockState>(false);

            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, int>(_moveInput.Value, builder.Get<IdleState>())
                .InitializeTransition<MoveLeftState, int>(_moveInput.Value, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, int>(_moveInput.Value, builder.Get<MoveRightState>())
                .InitializeTransition<JumpState, Unit>(_moveInput.JumpPressed, builder.Get<JumpState>())
                .InitializeTransition<MoveJumpState, int>(_moveInput.Value, builder.Get<MoveJumpState>())
                .InitializeTransition<PunchState, Unit>(_attackInputReader.PunchPressed, builder.Get<AttackState>())
                .InitializeTransition<KickState, Unit>(_attackInputReader.KickPressed, builder.Get<AttackState>())
                .InitializeTransition<UpHittedState, Unit>(_hitReader.TorsoHitted, builder.Get<UpHittedState>())
                .InitializeTransition<DownHittedState, Unit>(_hitReader.LegsHitted, builder.Get<DownHittedState>())
                .InitializeTransition<BlockState, Unit>(_attackInputReader.BlockPressed, builder.Get<BlockState>());
        }
    }
}