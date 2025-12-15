using System;
using System.Collections.Generic;
using FiniteStateMachine.States;

namespace Extensions
{
    public static class AttackStateComparer
    {
        private static readonly Dictionary<AttackType, Type> _attackState;
        
        static AttackStateComparer()
        {
            _attackState = new Dictionary<AttackType, Type>
            {
                { AttackType.UpAttack, typeof(UpAttackState) },
                { AttackType.DownAttack, typeof(DownAttackState) },
                { AttackType.Special, typeof(SpecialAttackState) },
                { AttackType.Super, typeof(SuperAttackState) },
            };
        }

        public static Type GetAttackState(AttackType attackType)
        {
            return _attackState[attackType];
        }
    }
}