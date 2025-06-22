using System;
using MovementSystem;
using R3;

namespace FiniteStateMachine.Conditions
{
    public class JumpCondition : Condition
    {
        private readonly Condition _condition;
        private readonly Jump _jump;
        private readonly bool _isJumping;
        
        public JumpCondition(Jump jump, bool isJumping, Condition condition)
        {
            if(jump == null)
                throw new ArgumentNullException(nameof(jump));
            
            _isJumping = isJumping;
            _jump = jump;
            _condition = condition;
        }
        
        public override Observable<Unit> GetCondition()
        {
            return _condition.GetCondition().Where(_ => _jump.IsExecuting == _isJumping);
        }
    }
}