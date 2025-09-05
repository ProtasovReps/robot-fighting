using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public class HandMeleeAttackImplant : HandAttackImplant
    {
        [SerializeField] private Spherecaster _spherecaster;
        
        protected override Attack ConstructAttack(Damage damage, AttackData attackData, LayerMask layerMask)
        {
            _spherecaster.Initialize(layerMask);
            return new MeleeAttack(damage, attackData, _spherecaster);
        }
    }
}