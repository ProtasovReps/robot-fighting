using System;
using System.Collections.Generic;
using System.Linq;
using Extensions.Exceptions;
using FiniteStateMachine.States;
using Interface;
using Unit = R3.Unit;

namespace FiniteStateMachine.Conditions
{
    public class ConditionBuilder : IConditionAddable
    {
        private readonly Dictionary<Type, Func<Unit, bool>> _bareConditions = new();
        private readonly Dictionary<Type, Condition> _buildedConditions = new();

        public void Add<TKeyState>(Func<Unit, bool> condition)
            where TKeyState : State
        {
            Type newKey = typeof(TKeyState);
            
            if (_bareConditions.ContainsKey(newKey))
            {
                throw new ArgumentException(nameof(newKey));
            }

            if (_buildedConditions.ContainsKey(newKey) == false)
            {
                _buildedConditions.Add(newKey, new Condition(condition));
            }

            _bareConditions.Add(newKey, condition);
        }

        public Func<Unit, bool> Get<TKeyState>()
            where TKeyState : State
        {
            return GetBuilded<TKeyState>().Current;
        }
        
        public Func<Unit, bool> GetBare<TCondition>()
            where TCondition : State
        {
            Type searchedCondition = typeof(TCondition);

            ValidateDictionary(_bareConditions, searchedCondition);
            return _bareConditions[searchedCondition];
        }

        public void Reset<TBuildableCondition>(bool isExecuting)
            where TBuildableCondition : State
        {
            Condition condition = GetBuilded<TBuildableCondition>();
            Func<Unit, bool> bareCondition = GetBare<TBuildableCondition>();

            condition.Reset(bareCondition, isExecuting);
        }

        public void Merge<TBuildableCondition, TBareCondition>(bool isExecuting = true)
            where TBuildableCondition : State
            where TBareCondition : State
        {
            Condition buildableCondition = GetBuilded<TBuildableCondition>();

            buildableCondition.Add(GetBare<TBareCondition>(), isExecuting);
        }

        public void MergeGlobal<TBareCondition>(bool isExecuting, params Type[] excludedConditions)
            where TBareCondition : State
        {
            foreach (var conditionPair in _buildedConditions)
            {
                if (excludedConditions.Contains(conditionPair.Key) || conditionPair.Key == typeof(TBareCondition))
                {
                    continue;
                }

                conditionPair.Value.Add(GetBare<TBareCondition>(), isExecuting);
            }
        }
       
        private Condition GetBuilded<TCondition>()
            where TCondition : State
        {
            Type searchedCondition = typeof(TCondition);

            ValidateDictionary(_buildedConditions, searchedCondition);
            return _buildedConditions[searchedCondition];
        }

        private void ValidateDictionary<T>(Dictionary<Type, T> dictionary, Type searchedType)
        {
            if (dictionary.ContainsKey(searchedType) == false)
            {
                throw new StateNotFoundException(nameof(searchedType));
            }
        }
    }
}