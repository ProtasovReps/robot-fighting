using FightingSystem.AttackDamage;
using UnityEngine;

namespace FightingSystem.Guns
{
    public class ProjectileFactory : MonoBehaviour
    {
        [SerializeField] private Projectile _prefab;
        
        private Damage _damage;
        private LayerMask _opponentLayer;

        public void Initialize(Damage damage, LayerMask opponentLayer)
        {
            _damage = damage;
            _opponentLayer = opponentLayer;
        }
        
        public Projectile Produce()
        {
            Projectile projectile = Instantiate(_prefab);
            
            projectile.Initialize(_opponentLayer, _damage);
            return projectile;
        }
    }
}