using FightingSystem.Attacks;

namespace FightingSystem.AttackDamage
{
    public struct Damage
    {
        public Damage(float value, float impulseForce, DamageType damageType)
        {
            Value = value;
            ImpulseForce = impulseForce;
            Type = damageType;
        }
        
        public float Value { get; }
        public float ImpulseForce { get; }
        public DamageType Type { get; }
    }
}