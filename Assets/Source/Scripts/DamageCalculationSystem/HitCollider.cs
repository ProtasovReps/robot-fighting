using Interface;
using UnityEngine;

namespace DamageCalculationSystem
{
    public class HitCollider : MonoBehaviour, IDamageable
    {
        private IDamageable _damageable;
        
        public void Initialize(IDamageable damageable)
        {
            _damageable = damageable;
        }
        
        public void AcceptDamage(float damage)
        {
            _damageable.AcceptDamage(damage);
        }
    }
}