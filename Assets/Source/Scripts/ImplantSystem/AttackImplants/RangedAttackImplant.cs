using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using FightingSystem.Guns;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public class RangedAttackImplant : AttackImplant
    {
        [SerializeField] private ProjectileShooter _projectileShooter;
        [SerializeField] private ProjectilePool _projectilePool;
        [SerializeField] private ProjectileFactory _projectileFactory;
        
        protected override Attack ConstructAttack(Damage damage, float duration, float delay, LayerMask layerMask)
        {
            _projectileFactory.Initialize(damage, layerMask);
            _projectilePool.Initialize(_projectileFactory);
            _projectileShooter.Initialize(_projectilePool);
            
            return new RangedAttack(duration, delay, _projectileShooter);
        }
    }
}