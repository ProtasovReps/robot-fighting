using Extensions;
using Interface;

namespace FightingSystem.Attacks.Melee
{
    public class MeleeAttack : Attack
    {
        private readonly Spherecaster _spherecaster;

        public MeleeAttack(Damage damage, AttackData attackData, Spherecaster spherecaster)
            : base(damage, attackData.Duration, attackData.Delay)
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