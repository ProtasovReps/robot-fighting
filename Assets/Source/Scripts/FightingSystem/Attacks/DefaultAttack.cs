using Interface;

namespace FightingSystem.Attacks
{
    public class DefaultAttack : IAttack
    {
        private readonly float _damage; // лучше получать из scriptable object или стат класса

        public float Delay { get; } // убрать от сюда и сделать scriptable object для атак
        

        public void ApplyDamage(IDamageable damageable)
        {
            damageable.AcceptDamage(_damage);
        }
    }
}