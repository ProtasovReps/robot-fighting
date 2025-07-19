using System;
using CharacterSystem.Data;
using FiniteStateMachine.States;
using InputSystem;
using Interface;
using R3;

namespace FiniteStateMachine.Factory
{
    public class BotStateMachineFactory : StateMachineFactory
    {
        private readonly BotInputReader _botInputReader;
        
        public BotStateMachineFactory(BotData botData)
            : base(botData.BotInputReader, botData)
        {
            _botInputReader = botData.BotInputReader;
        }

        protected override IState[] GetStates()
        {
            return new IState[]
            {
                new IdleState(),
                new MoveLeftState(),
                new MoveRightState(),
                new HittedState(),
                new PunchState(),
                new KickState()
            };
        }

        protected override void AddExtraConditions(ConditionBuilder builder)
        {
        }

        protected override void InitializeConditionTransition(ConditionBuilder builder,
            CharacterStateMachine stateMachine)
        {
            Func<Unit, bool> idleCondition = builder.Build(
                (typeof(IdleState), true),
                (typeof(HittedState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> moveLeftCondition = builder.Build(
                (typeof(MoveLeftState), true),
                (typeof(HittedState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> moveRightCondition = builder.Build(
                (typeof(MoveRightState), true),
                (typeof(HittedState), false),
                (typeof(AttackState), false));
            Func<Unit, bool> hittedCondition = builder.Build((typeof(HittedState), true));
            Func<Unit, bool> attackCondition = builder.Build((typeof(AttackState), false));

            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, float>(InputReader.Direction, idleCondition)
                .InitializeTransition<MoveLeftState, float>(InputReader.Direction, moveLeftCondition)
                .InitializeTransition<MoveRightState, float>(InputReader.Direction, moveRightCondition)
                .InitializeTransition<HittedState, float>(InputReader.Direction, hittedCondition) // Не должно из InputReader
                .InitializeTransition<PunchState, Unit>(_botInputReader.UpAttackPressed, attackCondition)
                .InitializeTransition<KickState, Unit>(_botInputReader.DownAttackPressed, attackCondition);
        }
    }
}