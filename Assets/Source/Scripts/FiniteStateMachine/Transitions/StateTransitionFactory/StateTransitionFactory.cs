using System;
using FiniteStateMachine.Conditions;
using Reflex.Attributes;
using UnityEngine;

namespace FiniteStateMachine.Transitions.Factory
{
    public abstract class StateTransitionFactory<TStateMachine, KCondtionBuilder> : MonoBehaviour
        where TStateMachine : StateMachine
        where KCondtionBuilder : ConditionBuilder // Disposer должен удалять инициализаторы
    {
        private ConditionBuilder _builder;
        private StateMachine _stateMachine;

        [Inject]
        private void Inject(TStateMachine stateMachine, KCondtionBuilder conditionBuilder)
        {
            _stateMachine = stateMachine;
            _builder = conditionBuilder;
        }

        private void Start()
        {
            if (_stateMachine == null)
                throw new ArgumentNullException(nameof(_stateMachine));

            if (_builder == null)
                throw new ArgumentNullException(nameof(_builder));

            InitializeTransitions();
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