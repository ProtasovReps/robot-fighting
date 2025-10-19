namespace FiniteStateMachine.Transitions.Factory
{
    public class SoloTransitionFactory : TransitionFactory
    {
        public override Transition<TTargetState> Produce<TTargetState>(StateMachine stateMachine)
        {
            return new SoloTransition<TTargetState>(stateMachine);
        }
    }
}