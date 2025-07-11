using System;
using FightingSystem;
using FiniteStateMachine.Conditions;
using FiniteStateMachine.States;
using InputSystem;
using Interface;
using MovementSystem;
using R3;

namespace FiniteStateMachine.Factory
{
    public class PlayerStateMachineFactory : StateMachineFactory<IPlayerStateMachine>
    {
        private readonly Jump _jump;
        private readonly Attacker _attacker;
        private readonly PlayerInputReader _inputReader;
        
        public PlayerStateMachineFactory(PlayerInputReader playerInputReader, Jump jump, Attacker attacker) : base(playerInputReader)
        {
            if (jump == null)
                throw new ArgumentNullException(nameof(jump));

            if (attacker == null)
                throw new ArgumentNullException(nameof(attacker));

            _jump = jump;
            _attacker = attacker;
            _inputReader = playerInputReader;
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

        protected override IPlayerStateMachine GetStateMachine(IState[] states)
        {
           return new CharacterStateMachine(states, states[0]);
        }

        protected override void AddExtraConditions(ConditionBuilder builder)
        {
            builder.Add(ConditionType.Jump, _ => _jump.IsExecuting);
            builder.Add(ConditionType.Attack, _ => _attacker.IsExecuting);
        }

        protected override void InitializeConditionTransition(ConditionBuilder builder, CharacterStateMachine stateMachine)
        {
            Func<Unit, bool> idleCondition = builder.Build(
                (ConditionType.Stay, true),
                (ConditionType.Jump, false),
                (ConditionType.Attack, false));
            Func<Unit, bool> moveLeftCondition = builder.Build(
                (ConditionType.MoveLeft, true),
                (ConditionType.Jump, false),
                (ConditionType.Attack, false));
            Func<Unit, bool> moveRightCondition = builder.Build(
                (ConditionType.MoveRight, true),
                (ConditionType.Jump, false),
                (ConditionType.Attack, false));
            Func<Unit, bool> moveJumpCondition = builder.Build(
                (ConditionType.Stay, false),
                (ConditionType.Jump, true),
                (ConditionType.Attack, false));
            Func<Unit, bool> jumpCondition = builder.Build(
                (ConditionType.Jump, false),
                (ConditionType.Attack, false));
            Func<Unit, bool> attackCondition = builder.Build((ConditionType.Attack, false));
            
            new TransitionInitializer(stateMachine) // disposeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee
                .InitializeTransition<IdleState, float>(InputReader.Direction, idleCondition)
                .InitializeTransition<MoveLeftState, float>(InputReader.Direction, moveLeftCondition)
                .InitializeTransition<MoveRightState, float>(InputReader.Direction, moveRightCondition)
                .InitializeTransition<JumpState, Unit>(_inputReader.JumpPressed, jumpCondition) // пока так, чтобы не ломалось из-за разницы с ботом 
                .InitializeTransition<MoveJumpState, float>(_inputReader.Direction, moveJumpCondition)
                .InitializeTransition<PunchState, Unit>(_inputReader.PunchPressed, attackCondition)
                .InitializeTransition<KickState, Unit>(_inputReader.KickPressed, attackCondition);
        }
    }
}