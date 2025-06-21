using System;
using InputSystem;
using MovementSystem;
using R3;

namespace FiniteStateMachine.TransitionConditions
{
    public class IdleCondition : Condition<float>
    {
        private readonly InputReader _inputReader;
        private readonly Jump _jump;
        
        public IdleCondition(InputReader inputReader, Jump jump)
        {
            if(inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));
            
            if(jump == null)
                throw new ArgumentNullException(nameof(jump));
            
            _inputReader = inputReader;
            _jump = jump;
        }
        
        public override Observable<float> GetCondition()
        {
            return _inputReader.Direction.Where(direction => direction == 0 && _jump.IsExecuting == false);
        }
    }
}