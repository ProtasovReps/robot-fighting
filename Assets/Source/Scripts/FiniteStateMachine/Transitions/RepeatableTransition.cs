using FiniteStateMachine.States;

namespace FiniteStateMachine.Transitions
{
    public class RepeatableTransition<TTargetState> : Transition<TTargetState>
        where TTargetState : State
    {
        public RepeatableTransition(StateMachine machine) 
            : base(machine)
        {
        }

        protected override bool ValidateState(State currentState)
        {
            return true;
        }
    }
}