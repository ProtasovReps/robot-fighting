using System;
using Extensions.Exceptions;
using FiniteStateMachine.States;
using Interface;
using R3;
using UnityEngine;

namespace FiniteStateMachine
{
    public class StateMachine : IStateMachine
    {
        private readonly State[] _states;
        private readonly ReactiveProperty<State> _currentState;

        protected StateMachine(State[] states)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states));

            if (states.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(states));

            _states = states;
            _currentState = new ReactiveProperty<State>(states[0]);
        }

        public ReadOnlyReactiveProperty<State> CurrentState => _currentState;

        public void Enter(Type newState)
        {
            State state = Array.Find(_states, state => state.Type == newState);

            if (state == null)
                throw new StateNotFoundException(nameof(newState));

            _currentState.OnNext(state);

            if (state.Type == typeof(NothingNearbyState)
                || state.Type == typeof(OpponentNearbyState) 
                || state.Type == typeof(WallNearbyState)
                || state.Type == typeof(WallOpponentNearbyState))
            {
                Debug.Log(state); // убрать потом
            }
        }
    }
}