namespace FightingSystem.AttackDamage
{
    public struct Damage
    {
        public Damage(float value, float impulseForce)
        {
            Value = value;
            ImpulseForce = impulseForce;
        }
        
        public float Value { get; }
        public float ImpulseForce { get; }
    }
}