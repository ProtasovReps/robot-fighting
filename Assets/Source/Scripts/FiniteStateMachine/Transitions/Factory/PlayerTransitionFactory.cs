using System;
using CharacterSystem.Data;
using FiniteStateMachine.States;
using InputSystem;
using R3;

namespace FiniteStateMachine.Transitions.Factory
{
    public class PlayerTransitionFactory : StateTransitionFactory
    {
        private readonly PlayerInputReader _inputReader;
        private readonly PlayerData _playerData;

        public PlayerTransitionFactory(PlayerData playerData)
            : base(playerData.PlayerInputReader, playerData)
        {
            if (playerData == null)
                throw new ArgumentNullException(nameof(playerData));

            _playerData = playerData;
            _inputReader = playerData.PlayerInputReader;
        }

        protected override void InitializeConditionTransition(
            ConditionBuilder builder, //нужно подумать, как создать основные условия внутри, а дополнительные собачить в наследниках
            CharacterStateMachine stateMachine)
        {
            AddCondition<JumpState>(_ => _playerData.Jump.IsExecuting);
            AddCondition<BlockState>(_ => _playerData.Fighter.Block.IsExecuting);
            
            Func<Unit, bool> idleCondition = builder.Build(
                (typeof(IdleState), true),
                (typeof(JumpState), false),
                (typeof(AttackState), false),
                (typeof(HittedState), false));
            Func<Unit, bool> moveLeftCondition = builder.Build(
                (typeof(MoveLeftState), true),
                (typeof(JumpState), false),
                (typeof(AttackState), false),
                (typeof(HittedState), false));
            Func<Unit, bool> moveRightCondition = builder.Build(
                (typeof(MoveRightState), true),
                (typeof(JumpState), false),
                (typeof(AttackState), false),
                (typeof(HittedState), false));
            Func<Unit, bool> moveJumpCondition = builder.Build(
                (typeof(IdleState), false),
                (typeof(JumpState), true),
                (typeof(AttackState), false),
                (typeof(HittedState), false));
            Func<Unit, bool> jumpCondition = builder.Build(
                (typeof(JumpState), false),
                (typeof(AttackState), false),
                (typeof(HittedState), false));
            Func<Unit, bool> attackCondition = builder.Build(
                (typeof(AttackState), false),
                (typeof(HittedState), false));
            ;
            Func<Unit, bool> hittedCondition = builder.Build((typeof(HittedState), true));
            Func<Unit, bool> blockCondition = builder.Build((typeof(BlockState), true));

            new TransitionInitializer(stateMachine) // disposeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                .InitializeTransition<IdleState, float>(_inputReader.Direction, idleCondition)
                .InitializeTransition<MoveLeftState, float>(_inputReader.Direction, moveLeftCondition)
                .InitializeTransition<MoveRightState, float>(_inputReader.Direction, moveRightCondition)
                .InitializeTransition<JumpState,
                    Unit>(_inputReader.JumpPressed, jumpCondition) // пока так, чтобы не ломалось из-за разницы с ботом 
                .InitializeTransition<MoveJumpState, float>(_inputReader.Direction, moveJumpCondition)
                .InitializeTransition<PunchState, Unit>(_inputReader.PunchPressed, attackCondition)
                .InitializeTransition<KickState, Unit>(_inputReader.KickPressed, attackCondition)
                .InitializeTransition<HittedState,
                    float>(_inputReader.Direction, hittedCondition) // не должно быть из Direction
                .InitializeTransition<BlockState, Unit>(_inputReader.BlockPressed, blockCondition);
        }
    }
}