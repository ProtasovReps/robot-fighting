using System;
using InputSystem;
using R3;
using UnityEngine;

namespace FiniteStateMachine.Conditions
{
    public class MoveCondition : Condition
    {
        private readonly InputReader _inputReader;
        private readonly Condition _condition;
        private readonly int _requiredDirection;

        public MoveCondition(InputReader inputReader, bool isMoving, Condition condition)
        {
            if (inputReader == null)
                throw new ArgumentNullException(nameof(inputReader));

            _requiredDirection = Convert.ToInt32(isMoving);
            _inputReader = inputReader;
            _condition = condition;
        }

        public override Observable<Unit> GetCondition()
        {
            return _condition.GetCondition().Where(_ =>
                Mathf.Approximately(Mathf.Abs(_inputReader.Direction.CurrentValue), _requiredDirection));
        }
    }
}