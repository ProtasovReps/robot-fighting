using System;
using CharacterSystem.Data;
using FiniteStateMachine.States;
using InputSystem;
using Interface;
using R3;

namespace FiniteStateMachine.Factory
{
    public class PlayerStateMachineFactory : StateMachineFactory
    {
        private readonly PlayerInputReader _inputReader;
        private readonly PlayerData _playerData;
        
        public PlayerStateMachineFactory(PlayerData playerData)
            : base(playerData.PlayerInputReader, playerData.Fighter)
        {
            if (playerData == null)
                throw new ArgumentNullException(nameof(playerData));

            _playerData = playerData;
            _inputReader = playerData.PlayerInputReader;
        }

        protected override IState[] GetStates()
        {
            return new IState[]
            {
                new IdleState(),
                new MoveLeftState(),
                new MoveRightState(),
                new JumpState(),
                new MoveJumpState(),
                new PunchState(),
                new KickState()
            };
        }

        protected override void AddExtraConditions(ConditionBuilder builder)
        {
            builder.Add<JumpState>(_ => _playerData.Jump.IsExecuting);
            builder.Add<AttackState>(_ => _playerData.Attacker.IsExecuting);
        }

        protected override void InitializeConditionTransition(ConditionBuilder builder,
            CharacterStateMachine stateMachine)
        {
            Func<Unit, bool> idleCondition = builder.Build(
                (typeof(IdleState), true),
                (typeof(JumpState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> moveLeftCondition = builder.Build(
                (typeof(MoveLeftState), true),
                (typeof(JumpState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> moveRightCondition = builder.Build(
                (typeof(MoveRightState), true),
                (typeof(JumpState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> moveJumpCondition = builder.Build(
                (typeof(IdleState), false),
                (typeof(JumpState), true),
                (typeof(AttackState), false));
            Func<Unit, bool> jumpCondition = builder.Build(
                (typeof(JumpState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> attackCondition = builder.Build((typeof(AttackState), false));

            new TransitionInitializer(stateMachine) // disposeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                .InitializeTransition<IdleState, float>(InputReader.Direction, idleCondition)
                .InitializeTransition<MoveLeftState, float>(InputReader.Direction, moveLeftCondition)
                .InitializeTransition<MoveRightState, float>(InputReader.Direction, moveRightCondition)
                .InitializeTransition<JumpState,
                    Unit>(_inputReader.JumpPressed, jumpCondition) // пока так, чтобы не ломалось из-за разницы с ботом 
                .InitializeTransition<MoveJumpState, float>(InputReader.Direction, moveJumpCondition)
                .InitializeTransition<PunchState, Unit>(_inputReader.PunchPressed, attackCondition)
                .InitializeTransition<KickState, Unit>(_inputReader.KickPressed, attackCondition);
        }
    }
}