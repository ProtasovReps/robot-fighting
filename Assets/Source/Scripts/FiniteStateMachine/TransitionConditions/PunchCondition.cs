using System;
using InputSystem;
using R3;

namespace FiniteStateMachine.TransitionConditions
{
    public class PunchCondition : Condition<Unit>
    {
        private readonly InputReader _inputReader;

        public PunchCondition(InputReader inputReader)
        {
            if (inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));
            
            _inputReader = inputReader;
        }
        
        public override Observable<Unit> GetCondition()
        {
            return _inputReader.PunchPressed;
        }
    }
}