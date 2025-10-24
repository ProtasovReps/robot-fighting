using CharacterSystem.Parameters;
using FightingSystem.AttackDamage;
using UnityEngine;

namespace FightingSystem.Factory
{
    public class BotAttackFactory : AttackFactory
    {
        [SerializeField] private BotParameters _botParameters;
        
        protected override Damage GetBaseDamage()
        {
            float damage = _botParameters.BaseDamage;
            float impulse = _botParameters.BaseImpulse;
            DamageType damageType = _botParameters.DamageType;

            return new Damage(damage, impulse, damageType);
        }
    }
}