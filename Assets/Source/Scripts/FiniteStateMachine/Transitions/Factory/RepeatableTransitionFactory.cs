namespace FiniteStateMachine.Transitions.Factory
{
    public class RepeatableTransitionFactory : TransitionFactory
    {
        public override Transition<TTargetState> Produce<TTargetState>(StateMachine stateMachine)
        {
            return new RepeatableTransition<TTargetState>(stateMachine);
        }
    }
}