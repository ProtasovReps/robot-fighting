using FiniteStateMachine.States;

namespace FiniteStateMachine.Factory
{
    public abstract class StateFactory
    {
        public State[] Produce()
        {
            return GetStates();
        }
    
        protected abstract State[] GetStates();
    }
}