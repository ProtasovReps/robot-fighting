using FiniteStateMachine.States;
using UnityEngine;

namespace FiniteStateMachine
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerStateMachine(State[] states) : base(states)
        {
        }
    }
}