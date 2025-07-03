using System;
using System.Collections.Generic;
using FightingSystem.Attacks;
using FiniteStateMachine.States;

namespace Extensions
{
    public static class RequiredStateFinder
    {
        private static readonly Dictionary<AttackType, Type> _states;
        
        static RequiredStateFinder()
        {
            _states = new Dictionary<AttackType, Type>
            {
                { AttackType.UpAttack, typeof(PunchState) },
                { AttackType.DownAttack, typeof(KickState) }
            };
        }
        
        public static Type GetState(AttackType attackType)
        {
            foreach (var attack in _states)
            {
                if (attack.Key == attackType)
                    return attack.Value;
            }
            
            throw new ArgumentException(nameof(attackType));
        } 
    }
}