using System;
using FiniteStateMachine.Conditions;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public abstract class StateTransitionFactory : MonoBehaviour
    {
        private CharacterStateMachine _stateMachine;
        private ConditionBuilder _builder;
        
        private void Start()
        {
            if (_stateMachine == null)
                throw new ArgumentNullException(nameof(_stateMachine));

            if (_builder == null)
                throw new ArgumentNullException(nameof(_builder));
            
            InitializeConditionTransition(_builder, _stateMachine);
        }

        public void Initialize(CharacterStateMachine stateMachine, ConditionBuilder conditionBuilder)
        {
            _stateMachine = stateMachine;
            _builder = conditionBuilder;
        }

        protected abstract void InitializeConditionTransition(ConditionBuilder builder, CharacterStateMachine stateMachine);
    }
}