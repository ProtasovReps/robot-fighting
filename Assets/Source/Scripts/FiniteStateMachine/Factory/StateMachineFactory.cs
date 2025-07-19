using System;
using CharacterSystem.Data;
using FiniteStateMachine.States;
using Interface;

// в целом, условия у всех наследников (например, для движения) одинаковые
namespace FiniteStateMachine.Factory
{
    public abstract class StateMachineFactory
    {
        protected StateMachineFactory(IDirectionChangeable inputReader, FighterData fighterData)
        {
            if (inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));

            if (fighterData == null)
                throw new ArgumentNullException(nameof(fighterData));
            
            InputReader = inputReader;
            FighterData = fighterData;
        }

        protected FighterData FighterData { get; }
        protected IDirectionChangeable InputReader { get; }

        public IStateMachine Produce()
        {
            IState[] states = GetStates(); // основные стейты здесь должны генерироваться, дополнительные добавлять наследники
            CharacterStateMachine machine = new(states);
            ConditionBuilder conditionBuilder = new();

            conditionBuilder.Add<IdleState>(_ => InputReader.Direction.CurrentValue == 0);
            conditionBuilder.Add<MoveLeftState>(_ => InputReader.Direction.CurrentValue < 0);
            conditionBuilder.Add<MoveRightState>( _ => InputReader.Direction.CurrentValue > 0);
            conditionBuilder.Add<HittedState>(_ => FighterData.Fighter.Stun.IsExecuting);
            conditionBuilder.Add<AttackState>(_ => FighterData.Attacker.IsExecuting);
            
            AddExtraConditions(conditionBuilder);
            InitializeConditionTransition(conditionBuilder, machine);
            return machine;
        }

        protected abstract IState[] GetStates();

        protected abstract void AddExtraConditions(ConditionBuilder builder);

        protected abstract void InitializeConditionTransition(ConditionBuilder builder,
            CharacterStateMachine stateMachine);
    }
}