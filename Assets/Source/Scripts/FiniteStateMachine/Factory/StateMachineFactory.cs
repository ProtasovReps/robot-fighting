using FiniteStateMachine.Conditions;
using Interface;

namespace FiniteStateMachine.Factory
{
    public abstract class StateMachineFactory<TMachine> 
    where TMachine : IStateMachine
    {
        protected StateMachineFactory(IDirectionChangeable inputReader)
        {
            InputReader = inputReader;
        }
        
        protected IDirectionChangeable InputReader { get; }
        
        public TMachine Produce()
        {
            IState[] states = GetStates();
            TMachine machine = GetStateMachine(states);
            ConditionBuilder conditionBuilder = new();
            
            conditionBuilder.Add(ConditionType.Stay, _ => InputReader.Direction.CurrentValue == 0);
            conditionBuilder.Add(ConditionType.MoveLeft, _ => InputReader.Direction.CurrentValue < 0);
            conditionBuilder.Add(ConditionType.MoveRight, _ => InputReader.Direction.CurrentValue > 0);

            AddExtraConditions(conditionBuilder);
            InitializeConditionTransition(conditionBuilder, machine as CharacterStateMachine);
            return machine;
        }
        
        protected abstract IState[] GetStates();

        protected abstract TMachine GetStateMachine(IState[] states);
        
        protected abstract void AddExtraConditions(ConditionBuilder builder);
        
        protected abstract void InitializeConditionTransition(ConditionBuilder builder, CharacterStateMachine stateMachine);
    }
}