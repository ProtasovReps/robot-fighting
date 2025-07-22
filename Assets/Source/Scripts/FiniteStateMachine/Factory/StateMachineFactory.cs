using Interface;

namespace FiniteStateMachine.Factory
{
    public abstract class StateMachineFactory
    {
        public CharacterStateMachine Produce()
        {
            IState[] states = GetStates();
            return new CharacterStateMachine(states);
        }
    
        protected abstract IState[] GetStates();
    }
}