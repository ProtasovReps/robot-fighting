using System;
using FiniteStateMachine.States;
using Interface;

// в целом, условия у всех наследников (например, для движения) одинаковые
namespace FiniteStateMachine.Factory
{
    public abstract class StateMachineFactory<TMachine>
        where TMachine : IStateMachine
    {
        protected StateMachineFactory(IDirectionChangeable inputReader)
        {
            if (inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));

            InputReader = inputReader;
        }

        protected IDirectionChangeable InputReader { get; }

        public TMachine Produce()
        {
            IState[] states = GetStates(); // основные стейты здесь должны генерироваться, дополнительные добавлять наследники
            TMachine machine = GetStateMachine(states);
            ConditionBuilder conditionBuilder = new();

            conditionBuilder.Add<IdleState>(_ => InputReader.Direction.CurrentValue == 0);
            conditionBuilder.Add<MoveLeftState>(_ => InputReader.Direction.CurrentValue < 0);
            conditionBuilder.Add<MoveRightState>( _ => InputReader.Direction.CurrentValue > 0);

            AddExtraConditions(conditionBuilder);
            InitializeConditionTransition(conditionBuilder, machine as CharacterStateMachine);
            return machine;
        }

        protected abstract IState[] GetStates();

        protected abstract TMachine GetStateMachine(IState[] states);

        protected abstract void AddExtraConditions(ConditionBuilder builder);

        protected abstract void InitializeConditionTransition(ConditionBuilder builder,
            CharacterStateMachine stateMachine);
    }
}