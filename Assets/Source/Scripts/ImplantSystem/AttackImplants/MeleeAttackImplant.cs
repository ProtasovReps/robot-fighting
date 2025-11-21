using Extensions;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public class MeleeAttackImplant : AttackImplant
    {
        [SerializeField] private Spherecaster _spherecaster;

        protected override Attack Construct(Damage damage, LayerMask layerMask)
        {
            _spherecaster.Initialize(layerMask);
            return new MeleeAttack(
                Parameters.Duration, Parameters.StartDelay, Parameters.EndDelay, damage, _spherecaster);
        }
    }
}