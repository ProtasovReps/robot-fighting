using System;
using CharacterSystem;
using FiniteStateMachine.States;
using Interface;

// в целом, условия у всех наследников (например, для движения) одинаковые
namespace FiniteStateMachine.Factory
{
    public abstract class StateMachineFactory
    {
        protected StateMachineFactory(IDirectionChangeable inputReader, Fighter fighter)
        {
            if (inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));

            if (fighter == null)
                throw new ArgumentNullException(nameof(fighter));
            
            InputReader = inputReader;
            Fighter = fighter;
        }

        protected Fighter Fighter { get; }
        protected IDirectionChangeable InputReader { get; }

        public IStateMachine Produce()
        {
            IState[] states = GetStates(); // основные стейты здесь должны генерироваться, дополнительные добавлять наследники
            IStateMachine machine = new CharacterStateMachine(states);
            ConditionBuilder conditionBuilder = new();

            conditionBuilder.Add<IdleState>(_ => InputReader.Direction.CurrentValue == 0);
            conditionBuilder.Add<MoveLeftState>(_ => InputReader.Direction.CurrentValue < 0);
            conditionBuilder.Add<MoveRightState>( _ => InputReader.Direction.CurrentValue > 0);
            conditionBuilder.Add<HittedState>(_ => Fighter.Stun.IsExecuting);
            
            AddExtraConditions(conditionBuilder);
            InitializeConditionTransition(conditionBuilder, machine as CharacterStateMachine);
            return machine;
        }

        protected abstract IState[] GetStates();

        protected abstract void AddExtraConditions(ConditionBuilder builder);

        protected abstract void InitializeConditionTransition(ConditionBuilder builder,
            CharacterStateMachine stateMachine);
    }
}