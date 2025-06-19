using Interface;

namespace FightingSystem.Attacks
{
    public class DefaultAttack : IAttack
    {
        private readonly float _damage = 10f; // лучше получать из scriptable object или стат класса

        public float Delay { get; } = 0.5f;// убрать от сюда и сделать scriptable object для атак
        
        public void ApplyDamage(IDamageable damageable)
        {
            damageable.AcceptDamage(_damage);
        }
    }
}