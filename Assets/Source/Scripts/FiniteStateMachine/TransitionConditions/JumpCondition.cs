using System;
using InputSystem;
using MovementSystem;
using R3;

namespace FiniteStateMachine.TransitionConditions
{
    public class JumpCondition : Condition<Unit>
    {
        private readonly InputReader _inputReader;
        private readonly Jump _jump;
        
        public JumpCondition(InputReader inputReader, Jump jump)
        {
            if(inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));
            
            if(jump == null)
                throw new ArgumentNullException(nameof(jump));
            
            _jump = jump;
            _inputReader = inputReader;
        }
        
        public override Observable<Unit> GetCondition()
        {
           return _inputReader.JumpPressed.Where(_ => _jump.IsExecuting == false);
        }
    }
}