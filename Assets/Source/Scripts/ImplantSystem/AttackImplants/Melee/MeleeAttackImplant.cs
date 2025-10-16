using Extensions;
using FightingSystem;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using UnityEngine;

namespace ImplantSystem.AttackImplants.Melee
{
    public abstract class MeleeAttackImplant : AttackImplant
    {
        [SerializeField] private Spherecaster _spherecaster;
        
        protected override Attack ConstructAttack(Damage damage, float duration, float delay, LayerMask layerMask)
        {
            _spherecaster.Initialize(layerMask);
            return new MeleeAttack(duration, delay, damage, _spherecaster);
        }
    }
}