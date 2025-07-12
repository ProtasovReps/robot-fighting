using System;
using System.Collections.Generic;
using Extensions.Exceptions;
using Interface;
using Unit = R3.Unit;

namespace FiniteStateMachine
{
    public class ConditionBuilder
    {
        private readonly Dictionary<Type, Func<Unit, bool>> _conditions = new();

        public void Add<TKeyState>(Func<Unit, bool> condition) where TKeyState : IState
        {
            Type newKey = typeof(TKeyState);
            
            if (_conditions.ContainsKey(newKey))
                throw new ArgumentException(nameof(newKey));

            _conditions.Add(newKey, condition);
        }

        public Func<Unit, bool> Build(params (Type, bool)[] conditions)
        {
            if (conditions.Length == 0)
                throw new ArgumentException(nameof(conditions));

            Func<Unit, bool> currentCondition = unit => Get(conditions[0].Item1)(unit) == conditions[0].Item2;
            int startIteratorValue = 1;

            for (int i = startIteratorValue; i < conditions.Length; i++)
            {
                Type currentKey = conditions[i].Item1;
                bool currentValue = conditions[i].Item2;
                Func<Unit, bool> tempCondition = currentCondition;

                currentCondition = unit => tempCondition(unit) && Get(currentKey)(unit) == currentValue;
            }

            return currentCondition;
        }

        private Func<Unit, bool> Get(Type searchedType)
        {
            if (_conditions.ContainsKey(searchedType) == false)
                throw new StateNotFoundException(nameof(searchedType));

            return _conditions[searchedType];
        }
    }
}