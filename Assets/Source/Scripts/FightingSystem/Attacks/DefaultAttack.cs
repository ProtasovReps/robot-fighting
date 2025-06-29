using Interface;

namespace FightingSystem.Attacks
{
    public class DefaultAttack : IAttack
    {
        private readonly float _damage;

        public DefaultAttack(float damage, float delay)
        {
            _damage = damage;
            Delay = delay;
        }

        public float Delay { get; } 

        public void ApplyDamage(IDamageable damageable)
        {
            damageable.AcceptDamage(_damage);
        }
    }
}