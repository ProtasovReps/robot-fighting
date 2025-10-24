using UnityEngine;

namespace FightingSystem.AttackDamage
{
    public class DamageFactory : MonoBehaviour
    {
        [SerializeField] private DamageStats _damageStats;

        public Damage Produce(Damage baseDamage)
        {
            float newDamage = _damageStats.Damage + baseDamage.Value;
            float newImpulseForce = _damageStats.ImpulseForce + baseDamage.ImpulseForce;
            DamageType damageType = baseDamage.Type;

            if (_damageStats.DamageType != DamageType.Default)
                damageType = _damageStats.DamageType;
            
            return new Damage(newDamage, newImpulseForce, damageType);
        }
    }
}