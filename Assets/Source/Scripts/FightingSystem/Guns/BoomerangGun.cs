using System.Collections.Generic;
using R3;
using UnityEngine;

namespace FightingSystem.Guns
{
    public class BoomerangGun : Gun
    {
        private readonly Dictionary<Projectile, Boomerang> _boomerangs = new ();
        
        [SerializeField] private float _pivotDelay;

        public override void Initialize(ProjectilePool projectilePool)
        {
            Executed
                .Subscribe(projectile => _boomerangs[projectile].Reset())
                .AddTo(this);
            
            base.Initialize(projectilePool);
        }

        protected override void TranslateProjectile(Projectile projectile, Vector3 direction, float force)
        {
            if (_boomerangs.ContainsKey(projectile) == false)
            {
                _boomerangs.Add(projectile, new Boomerang(_pivotDelay, direction));
            }

            Boomerang boomerang = _boomerangs[projectile];
            
            boomerang.Tick();
            direction = boomerang.GetDirection();
            
            base.TranslateProjectile(projectile, direction, force);
        }
    }
}