using FightingSystem.AttackDamage;

namespace FightingSystem.Factory
{
    public class PlayerAttackFactory : AttackFactory
    {
        protected override Damage GetBaseDamage()
        {
            return new Damage(0f, 0f, DamageType.Default); // заглушка
        }
    }
}