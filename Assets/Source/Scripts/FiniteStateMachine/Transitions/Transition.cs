using System;
using FiniteStateMachine.States;

namespace FiniteStateMachine.Transitions
{
    public class Transition<TTargetState>
        where TTargetState : State
    {
        private readonly StateMachine _machine;

        public Transition(StateMachine machine)
        {
            if (machine == null)
                throw new ArgumentNullException(nameof(machine));

            _machine = machine;
        }

        public void Transit()
        {
            if (_machine.CurrentState.CurrentValue.Type == typeof(TTargetState))
                return;

            _machine.Enter(typeof(TTargetState));
        }
    }
}