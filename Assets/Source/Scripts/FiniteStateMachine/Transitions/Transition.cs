using System;
using FiniteStateMachine.States;

namespace FiniteStateMachine.Transitions
{
    public abstract class Transition<TTargetState>
        where TTargetState : State
    {
        private readonly StateMachine _machine;

        protected Transition(StateMachine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            _machine = machine;
        }

        public void Transit()
        {
            if (ValidateState(_machine.Value.CurrentValue) == false)
                return;

            _machine.Enter(typeof(TTargetState));
        }

        protected abstract bool ValidateState(State currentState);
    }
}