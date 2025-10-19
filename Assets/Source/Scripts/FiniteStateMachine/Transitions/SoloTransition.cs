using FiniteStateMachine.States;

namespace FiniteStateMachine.Transitions
{
    public class SoloTransition<TTargetState> : Transition<TTargetState>
    where TTargetState : State
    {
        public SoloTransition(StateMachine machine)
            : base(machine)
        {
        }

        protected override bool ValidateState(State currentState)
        {
            return currentState.Type != typeof(TTargetState);
        }
    }
}