using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using FightingSystem.Guns;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public class RangedAttackImplant : AttackImplant
    {
        [SerializeField] private ProjectileShooter _shooter;
        [SerializeField] private ProjectilePool _projectilePool;
        [SerializeField] private ProjectileFactory _projectileFactory;

        protected override Attack Construct(Damage damage, LayerMask layerMask)
        {
            _projectileFactory.Initialize(damage, layerMask);
            _projectilePool.Initialize(_projectileFactory);
            _shooter.Initialize(_projectilePool);
            return new RangedAttack(Parameters.Duration, Parameters.StartDelay, Parameters.EndDelay, _shooter);
        }
    }
}