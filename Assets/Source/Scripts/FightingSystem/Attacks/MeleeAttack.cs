using Extensions;
using Interface;

namespace FightingSystem.Attacks.Melee
{
    public class MeleeAttack : Attack
    {
        private readonly Spherecaster _spherecaster;

        public MeleeAttack(Damage damage, float delay, float duration, Spherecaster spherecaster)
            : base(damage, duration, delay)
        {
            _spherecaster = spherecaster;
        }

        protected override void Execute(Damage damage)
        {
            if (_spherecaster.TryFindDamageable(out IDamageable<Damage> damageable) == false)
                return;
            
            damageable.AcceptDamage(damage);
        }
    }
}