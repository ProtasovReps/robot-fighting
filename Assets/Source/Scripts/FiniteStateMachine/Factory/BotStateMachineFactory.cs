using System;
using CharacterSystem.Data;
using FiniteStateMachine.States;
using Interface;
using R3;

namespace FiniteStateMachine.Factory
{
    public class BotStateMachineFactory : StateMachineFactory
    {
        public BotStateMachineFactory(BotData botData)
            : base(botData.BotInputReader, botData.Fighter)
        {
        }

        protected override IState[] GetStates()
        {
            return new IState[]
            {
                new IdleState(),
                new MoveLeftState(),
                new MoveRightState(),
                new HittedState()
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
                (typeof(HittedState), false));
            Func<Unit, bool> moveLeftCondition = builder.Build(
                (typeof(MoveLeftState), true),
                (typeof(HittedState), false));
            Func<Unit, bool> moveRightCondition = builder.Build(
                (typeof(MoveRightState), true),
                (typeof(HittedState), false));
            Func<Unit, bool> hittedCondition = builder.Build((typeof(HittedState), true));

            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, float>(InputReader.Direction, idleCondition)
                .InitializeTransition<MoveLeftState, float>(InputReader.Direction, moveLeftCondition)
                .InitializeTransition<MoveRightState, float>(InputReader.Direction, moveRightCondition)
                .InitializeTransition<HittedState, float>(InputReader.Direction, hittedCondition);
        }
    }
}