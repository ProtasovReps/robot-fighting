using System;
using FiniteStateMachine.Conditions;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public abstract class StateTransitionFactory : MonoBehaviour // Disposer должен удалять инициализаторы
    {
        private ConditionBuilder _builder;
        private StateMachine _stateMachine;

        private void Start()
        {
            if (_stateMachine == null)
                throw new ArgumentNullException(nameof(_stateMachine));

            if (_builder == null)
                throw new ArgumentNullException(nameof(_builder));

            InitializeTransitions();
        }

        public void Initialize(StateMachine stateMachine, ConditionBuilder conditionBuilder)
        {
            _stateMachine = stateMachine;
            _builder = conditionBuilder;
        }
        
        protected abstract void InitializeConditions(ConditionBuilder builder);
        protected abstract void InstallTransitions(StateMachine stateMachine, ConditionBuilder builder);
        
        private void InitializeTransitions()
        {
            InitializeConditions(_builder);
            InstallTransitions(_stateMachine, _builder);
        }
    }
}