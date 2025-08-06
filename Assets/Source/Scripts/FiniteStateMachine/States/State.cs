using System;

namespace FiniteStateMachine.States
{
    public abstract class State
    {
        protected State()
        {
            Type = GetType();
        }
        
        public Type Type { get; }
    }
}