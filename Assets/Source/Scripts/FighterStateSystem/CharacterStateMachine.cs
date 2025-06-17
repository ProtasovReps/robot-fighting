using System;
using Extensions.Exceptions;
using Interface;
using UnityEngine;

namespace FighterStateSystem
{
    public class CharacterStateMachine : IStateChangeable
    {
        private readonly IState[] _states;
        
        private IState _currentState;
        
        public CharacterStateMachine(IState[] states, IState startState)
        {
            if (states == null)
                throw new ArgumentNullException(nameof(states));
            
            if(states.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(states));
            
            if(startState == null)
                throw new ArgumentNullException(nameof(startState));
            
            _states = states;
            _currentState = startState;
        }

        public Type CurrentState => _currentState.Type;

        public void Enter(Type newState)
        {
            IState state = Array.Find(_states, state => state.Type == newState);

            if (state == null)
                throw new StateNotFoundException(nameof(newState));
            
            _currentState = state;
            Debug.Log(state); // убрать потом
        }
    }
}