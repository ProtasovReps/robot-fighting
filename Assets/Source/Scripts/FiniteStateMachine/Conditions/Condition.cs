using System;
using R3;

namespace FiniteStateMachine.Conditions
{
    public class Condition
    {
        public Condition(Func<Unit, bool> condition)
        {
            if (condition == null)
                throw new ArgumentNullException(nameof(condition));
            
            Current = condition;
        }

        public Func<Unit, bool> Current { get; private set; }

        public void Add(Func<Unit, bool> newCondition, bool isExecuting)
        {
            Func<Unit, bool> tempCondition = Current;
            Current = unit => tempCondition(unit) && newCondition(unit) == isExecuting;
        }

        public void Reset(Func<Unit, bool> newCondition, bool isExecuting)
        {
            Current = unit => newCondition(unit) == isExecuting;
        }
    }
}