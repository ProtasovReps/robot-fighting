using Extensions;
using FightingSystem.AttackDamage;
using Interface;
using UnityEngine;

namespace FightingSystem.Guns
{
    public class Overlaper : Shooter
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private Spherecaster _spherecaster;
        private Damage _damage;
        
        public void Initialize(Damage damage, Spherecaster spherecaster)
        {
            _damage = damage;
            _spherecaster = spherecaster;
        }
        
        public override void Shoot()
        {
            _particleSystem.Play();

            if (_spherecaster.TryFindDamageable(out IDamageable<Damage> damageable) == false)
                return;

            damageable.AcceptDamage(_damage);
        }
    }
}