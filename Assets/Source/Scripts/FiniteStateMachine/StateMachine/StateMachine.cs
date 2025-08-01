using System;
using Extensions.Exceptions;
using Interface;
using R3;
using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine : IStateMachine
    {
        private readonly IState[] _states;
        private readonly ReactiveProperty<IState> _currentState;

        protected StateMachine(IState[] states)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states));

            if (states.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(states));

            _states = states;
            _currentState = new ReactiveProperty<IState>(states[0]);
        }

        public ReadOnlyReactiveProperty<IState> CurrentState => _currentState;

        public void Enter(Type newState)
        {
            IState state = Array.Find(_states, state => state.Type == newState);

            if (state == null)
                throw new StateNotFoundException(nameof(newState));

            _currentState.OnNext(state);
            Debug.Log(state); // убрать потом
        }
    }
}