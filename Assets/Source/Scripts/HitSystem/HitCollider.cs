using FightingSystem.AttackDamage;
using Interface;
using UnityEngine;

namespace HitSystem
{
    public class HitCollider : MonoBehaviour, IDamageable<Damage>
    {
        private IDamageable<Damage> _damageable;
        
        public void Initialize(IDamageable<Damage> damageable)
        {
            _damageable = damageable;
        }
        
        public void AcceptDamage(Damage damage)
        {
            _damageable.AcceptDamage(damage);
        }
    }
}