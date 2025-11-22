using UnityEngine;

namespace FightingSystem.AttackDamage
{
    public class DamageFactory : MonoBehaviour
    {
        [SerializeField] private float _impulseForce;

        public Damage Produce(Damage baseDamage)
        {
            float newImpulseForce = _impulseForce + baseDamage.ImpulseForce;
            
            return new Damage(baseDamage.Value, newImpulseForce);
        }
    }
}