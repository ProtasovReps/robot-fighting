using System;
using Interface;

namespace FiniteStateMachine.Transitions
{
    public class Transition<TTargetState>
        where TTargetState : IState
    {
        private readonly CharacterStateMachine _machine;

        public Transition(CharacterStateMachine machine)
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