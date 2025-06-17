using System;
using Interface;

namespace FighterStateSystem.States
{
    public abstract class State : IState
    {
        protected State()
        {
            Type = GetType();
        }
        
        public Type Type { get; }
    }
}