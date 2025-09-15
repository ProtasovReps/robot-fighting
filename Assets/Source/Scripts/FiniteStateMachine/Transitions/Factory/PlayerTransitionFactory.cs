using FightingSystem;
using FightingSystem.Dying;
using HitSystem;
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
        private PlayerDeath _death;

        [Inject]
        private void Inject(PlayerMoveInputReader moveInput, PlayerAttackInputReader attackInputReader, PlayerDeath death)
        {
            _moveInput = moveInput;
            _attackInputReader = attackInputReader;
            _death = death;
        }

        protected override void InitializeConditionTransition(
            ConditionBuilder builder,
            TransitionInitializer initializer)
        {
            builder.Reset<JumpState>(false);
            builder.Reset<AttackState>(false);
            builder.Reset<BlockState>(false);

            builder.Add<MoveJumpState>(builder.GetBare<JumpState>());
            builder.Merge<MoveJumpState, IdleState>(false);

            builder.Merge<StretchState, IdleState>();
            builder.Merge<IdleState, StretchState>(false);

            builder.MergeGlobal<DeathState>(false);
            builder.MergeGlobal<JumpState>(false, typeof(MoveJumpState));
            builder.MergeGlobal<UpHittedState>(false, typeof(DownHittedState), typeof(DeathState));
            builder.MergeGlobal<DownHittedState>(false, typeof(UpHittedState), typeof(DeathState));
            builder.MergeGlobal<AttackState>(false, typeof(UpHittedState), typeof(DownHittedState));
            builder.MergeGlobal<BlockState>(false, typeof(DownHittedState), typeof(DeathState));

            initializer
                .InitializeTransition<IdleState, int>(_moveInput.Value, builder.Get<IdleState>())
                .InitializeTransition<StretchState, int>(_moveInput.Value, builder.Get<StretchState>())
                .InitializeTransition<MoveLeftState, int>(_moveInput.Value, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, int>(_moveInput.Value, builder.Get<MoveRightState>())
                .InitializeTransition<JumpState, Unit>(_moveInput.JumpPressed, builder.Get<JumpState>())
                .InitializeTransition<MoveJumpState, int>(_moveInput.Value, builder.Get<MoveJumpState>())
                .InitializeTransition<UpAttackState, Unit>(_attackInputReader.PunchPressed, builder.Get<AttackState>())
                .InitializeTransition<DownAttackState, Unit>(_attackInputReader.KickPressed, builder.Get<AttackState>())
                .InitializeTransition<SuperAttackState, Unit>(_attackInputReader.SuperPressed,
                    builder.Get<SuperAttackState>())
                .InitializeTransition<UpHittedState, Unit>(_hitReader.TorsoHitted, builder.Get<UpHittedState>())
                .InitializeTransition<DownHittedState, Unit>(_hitReader.LegsHitted, builder.Get<DownHittedState>())
                .InitializeTransition<BlockState, Unit>(_attackInputReader.BlockPressed, builder.Get<BlockState>())
                .InitializeTransition<UpDeathState, Unit>(_death.UpDeath, builder.Get<DeathState>())
                .InitializeTransition<DownDeathState, Unit>(_death.DownDeath, builder.Get<DeathState>());
        }
    }
}