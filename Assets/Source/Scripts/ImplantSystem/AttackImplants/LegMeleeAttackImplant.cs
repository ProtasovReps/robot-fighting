using System;
using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using FiniteStateMachine.States;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public class LegMeleeAttackImplant : AttackImplant
    {
        [SerializeField] private Spherecaster _spherecaster;
        
        public override Type RequiredState => typeof(DownAttackState);
        
        protected override Attack ConstructAttack(Damage damage, AttackData attackData, LayerMask layerMask)
        {
            _spherecaster.Initialize(layerMask);
            return new MeleeAttack(damage, attackData, _spherecaster);
        }
    }
}