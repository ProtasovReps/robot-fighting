using Interface;

namespace FiniteStateMachine.Factory
{
    public abstract class StateFactory
    {
        public IState[] Produce()
        {
            return GetStates();
        }
    
        protected abstract IState[] GetStates();
    }
}