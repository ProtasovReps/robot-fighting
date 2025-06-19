using System;
using Extensions.Exceptions;
using Interface;
using R3;
using UnityEngine;

namespace FiniteStateMachine
{
    public class CharacterStateMachine : IStateChangeable
    {
        private readonly IState[] _states;

        private ReactiveProperty<IState> _currentState;
        
        public CharacterStateMachine(IState[] states, IState startState)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states));
            
            if(states.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(states));
            
            if(startState == null)
                throw new ArgumentNullException(nameof(startState));
            
            _states = states;
            _currentState = new ReactiveProperty<IState>(startState);
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