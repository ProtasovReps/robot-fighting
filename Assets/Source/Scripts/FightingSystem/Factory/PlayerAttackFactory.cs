using FightingSystem.AttackDamage;
using YG;

namespace FightingSystem.Factory
{
    public class PlayerAttackFactory : AttackFactory
    {
        protected override Damage GetBaseDamage()
        {
            float damage = YG2.saves.DamageStat;

            return new Damage(damage, 0);
        }
    }
}