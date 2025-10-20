using Extensions;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using FightingSystem.Guns;
using UnityEngine;

namespace ImplantSystem.AttackImplants.Overlap
{
    public abstract class OverlapAttackImplant : AttackImplant
    {
        [SerializeField] private Overlaper _overlaper;
        [SerializeField] private Spherecaster _spherecaster;
        
        protected override Attack ConstructAttack(Damage damage, float duration, float delay, LayerMask layerMask)
        {
            _spherecaster.Initialize(layerMask);
            _overlaper.Initialize(damage, _spherecaster);
            
            return new RangedAttack(duration, delay, _overlaper);
        }
    }
}