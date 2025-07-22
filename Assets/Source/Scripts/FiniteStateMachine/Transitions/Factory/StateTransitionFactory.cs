using System;
using CharacterSystem.Data;
using FiniteStateMachine.States;
using Interface;
using R3;

namespace FiniteStateMachine.Transitions.Factory
{
    public abstract class StateTransitionFactory
    {
        private readonly IDirectionChangeable _directionChangeable;
        private readonly FighterData _fighterData;
        private readonly ConditionBuilder _conditionBuilder;
        
        protected StateTransitionFactory(IDirectionChangeable inputReader, FighterData fighterData)
        {
            if (inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));
        
            if (fighterData == null)
                throw new ArgumentNullException(nameof(fighterData));
            
            _directionChangeable = inputReader;
            _fighterData = fighterData;
            _conditionBuilder = new();
        }

        public void InstallMachine(CharacterStateMachine stateMachine)
        {
            _conditionBuilder.Add<IdleState>(_ => _directionChangeable.Direction.CurrentValue == 0);
            _conditionBuilder.Add<MoveLeftState>(_ => _directionChangeable.Direction.CurrentValue < 0);
            _conditionBuilder.Add<MoveRightState>( _ => _directionChangeable.Direction.CurrentValue > 0);
            _conditionBuilder.Add<HittedState>(_ => _fighterData.Fighter.Stun.IsExecuting);
            _conditionBuilder.Add<AttackState>(_ => _fighterData.Attacker.IsExecuting);
            
            InitializeConditionTransition(_conditionBuilder, stateMachine);
        }

        protected void AddCondition<TKeyState>(Func<Unit, bool> condition) where TKeyState : IState
        {
            _conditionBuilder.Add<TKeyState>(condition);
        }

        protected abstract void InitializeConditionTransition(ConditionBuilder builder, CharacterStateMachine stateMachine);
    }
}