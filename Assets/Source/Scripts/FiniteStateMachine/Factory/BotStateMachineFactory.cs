using System;
using FiniteStateMachine.States;
using InputSystem;
using Interface;
using R3;

namespace FiniteStateMachine.Factory
{
    public class BotStateMachineFactory : StateMachineFactory<IBotStateMachine>
    {
        public BotStateMachineFactory(BotInputReader botInput)
            : base(botInput)
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

        protected override IBotStateMachine GetStateMachine(IState[] states)
        {
            return new CharacterStateMachine(states, states[1]); //1
        }

        protected override void AddExtraConditions(ConditionBuilder builder)
        {
        }

        protected override void InitializeConditionTransition(ConditionBuilder builder,
            CharacterStateMachine stateMachine)
        {
            Func<Unit, bool> idleCondition = builder.Build((typeof(IdleState), true));
            Func<Unit, bool> moveLeftCondition = builder.Build((typeof(MoveLeftState), true));
            Func<Unit, bool> moveRightCondition = builder.Build((typeof(MoveRightState), true));

            new TransitionInitializer(stateMachine) // dispose
                .InitializeTransition<IdleState, float>(InputReader.Direction, idleCondition)
                .InitializeTransition<MoveLeftState, float>(InputReader.Direction, moveLeftCondition)
                .InitializeTransition<MoveRightState, float>(InputReader.Direction, moveRightCondition);
        }
    }
}