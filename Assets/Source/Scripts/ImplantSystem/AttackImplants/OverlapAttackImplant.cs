using Extensions;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using FightingSystem.Guns;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public class OverlapAttackImplant : AttackImplant
    {
        [SerializeField] private Overlaper _overlaper;
        [SerializeField] private Spherecaster _spherecaster;

        protected override Attack Construct(Damage damage, LayerMask layerMask)
        {
            _spherecaster.Initialize(layerMask);
            _overlaper.Initialize(damage, _spherecaster);
            return new RangedAttack(Parameters.Duration, Parameters.StartDelay, Parameters.EndDelay, _overlaper);
        }
    }
}