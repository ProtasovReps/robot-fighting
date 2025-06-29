using System;
using System.Collections.Generic;
using Unit = R3.Unit;

namespace FiniteStateMachine.Conditions
{
    public class ConditionBuilder
    {
        private readonly Dictionary<ConditionType, Func<Unit, bool>> _conditions = new();

        public void Add(ConditionType newType, Func<Unit, bool> condition)
        {
            if (_conditions.ContainsKey(newType))
                throw new ArgumentException(nameof(newType));

            _conditions.Add(newType, condition);
        }

        public Func<Unit, bool> Build(params (ConditionType, bool)[] conditions)
        {
            if (conditions.Length == 0)
                throw new ArgumentException(nameof(conditions));

            Func<Unit, bool> currentCondition = unit => Get(conditions[0].Item1)(unit) == conditions[0].Item2;
            int startIteratorValue = 1;
            
            for (int i = startIteratorValue; i < conditions.Length; i++)
            {
                ConditionType currentKey = conditions[i].Item1;
                bool currentValue = conditions[i].Item2;
                Func<Unit, bool> tempCondition = currentCondition;
                
                currentCondition = unit => tempCondition(unit) && Get(currentKey)(unit) == currentValue;
            }
            
            return currentCondition;
        }
        
        private Func<Unit, bool> Get(ConditionType searchedType)
        {
            if (_conditions.ContainsKey(searchedType) == false)
                throw new ArgumentException(nameof(searchedType));

            return _conditions[searchedType];
        }
    }
}