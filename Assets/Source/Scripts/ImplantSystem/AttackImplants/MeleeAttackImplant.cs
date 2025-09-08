using Extensions;
using FightingSystem;
using FightingSystem.Attacks;
using FightingSystem.Attacks.Melee;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public abstract class MeleeAttackImplant : AttackImplant
    {
        [SerializeField] private Spherecaster _spherecaster;
        
        protected override Attack ConstructAttack(Damage damage, AttackData attackData, LayerMask layerMask)
        {
            _spherecaster.Initialize(layerMask);
            return new MeleeAttack(damage, attackData, _spherecaster);
        }
    }
}