using FiniteStateMachine.States;

namespace FiniteStateMachine.Transitions.Factory
{
    public abstract class TransitionFactory
    {
        public abstract Transition<TTargetState> Produce<TTargetState>(StateMachine stateMachine)
            where TTargetState : State;
    }
}