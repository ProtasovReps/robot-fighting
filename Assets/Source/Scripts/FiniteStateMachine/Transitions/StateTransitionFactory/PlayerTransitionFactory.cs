using CharacterSystem.Dying;
using Extensions;
using HitSystem;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using InputSystem;
using R3;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public class PlayerTransitionFactory : StateTransitionFactory<PlayerStateMachine, PlayerConditionBuilder>
    {
        [SerializeField] private HitReader _hitReader;

        private PlayerMoveInputReader _moveInput;
        private PlayerAttackInputReader _attackInputReader;
        private PlayerDeath _death;

        public void Initialize(PlayerMoveInputReader moveInput, PlayerAttackInputReader inputReader, PlayerDeath death)
        {
            _moveInput = moveInput;
            _attackInputReader = inputReader;
            _death = death;
        }

        protected override void InitializeConditions(ConditionBuilder builder)
        {
            builder.Reset<JumpState>(false);
            builder.Reset<AttackState>(false);
            builder.Reset<BlockState>(false);

            builder.Add<MoveJumpState>(builder.GetBare<JumpState>());
            builder.Merge<MoveJumpState, IdleState>(false);

            builder.Merge<StretchState, IdleState>();
            builder.Merge<IdleState, StretchState>(false);

            builder.MergeGlobal<DeathState>(false);

            builder.MergeGlobal<JumpState>(
                false, typeof(MoveJumpState), typeof(DeathState),
                typeof(DownHittedState), typeof(UpHittedState));
            builder.MergeGlobal<UpHittedState>(
                false, typeof(DownHittedState), typeof(DeathState));
            builder.MergeGlobal<DownHittedState>(
                false, typeof(UpHittedState), typeof(DeathState));
            builder.MergeGlobal<AttackState>(
                false, typeof(UpHittedState), typeof(DownHittedState),
                typeof(JumpState), typeof(BlockState), typeof(MoveLeftState));
            builder.MergeGlobal<BlockState>(
                false, typeof(DownHittedState), typeof(DeathState));
        }

        protected override void InstallTransitions(StateMachine machine, ConditionBuilder builder, Disposer disposer)
        {
            var soloInitializer = new TransitionInitializer(new SoloTransitionFactory(), machine)
                .InitializeTransition<IdleState, float>(
                    _moveInput.Value, builder.Get<IdleState>())
                .InitializeTransition<StretchState, float>(
                    _moveInput.Value, builder.Get<StretchState>())
                .InitializeTransition<MoveLeftState, float>(
                    _moveInput.Value, builder.Get<MoveLeftState>())
                .InitializeTransition<MoveRightState, float>(
                    _moveInput.Value, builder.Get<MoveRightState>())
                .InitializeTransition<JumpState, Unit>(
                    _moveInput.JumpPressed, builder.Get<JumpState>())
                .InitializeTransition<MoveJumpState, float>(
                    _moveInput.Value, builder.Get<MoveJumpState>())
                .InitializeTransition<UpDeathState, Unit>(
                    _death.UpDeath, builder.Get<DeathState>())
                .InitializeTransition<DownDeathState, Unit>(
                    _death.DownDeath, builder.Get<DeathState>());

            var repeatableInitializer = new TransitionInitializer(new RepeatableTransitionFactory(), machine)
                .InitializeTransition<UpAttackState, Unit>(
                    _attackInputReader.PunchPressed, builder.Get<AttackState>())
                .InitializeTransition<DownAttackState, Unit>(
                    _attackInputReader.KickPressed, builder.Get<AttackState>())
                .InitializeTransition<SuperAttackState, Unit>(
                    _attackInputReader.SuperPressed, builder.Get<SuperAttackState>())
                .InitializeTransition<BlockState, Unit>(
                    _attackInputReader.BlockPressed, builder.Get<BlockState>())
                .InitializeTransition<UpHittedState, Unit>(
                    _hitReader.TorsoHitted, builder.Get<UpHittedState>())
                .InitializeTransition<DownHittedState, Unit>(
                    _hitReader.LegsHitted, builder.Get<DownHittedState>());

            disposer.Add(soloInitializer);
            disposer.Add(repeatableInitializer);
        }
    }
}