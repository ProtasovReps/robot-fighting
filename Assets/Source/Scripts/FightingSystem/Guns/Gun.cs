using UnityEngine;

namespace FightingSystem.Guns
{
    public class Gun : ProjectileShooter
    {
        protected override void TranslateProjectile(Projectile projectile, Vector3 direction, float force)
        {
            projectile.transform.position += direction * force * Time.deltaTime;
        }
    }
}