using FightingSystem;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public abstract class AttackImplant : MonoBehaviour
    {
        [SerializeField] private DamageFactory _damageFactory;
        
        [field: SerializeField] public AttackParameters AttackParameters;

        public Attack GetAttack(LayerMask opponentLayerMask, Damage baseDamage)
        {
            Damage damage = _damageFactory.Produce(baseDamage);
            return ConstructAttack(damage, AttackParameters.Duration, AttackParameters.Delay, opponentLayerMask);
        }

        protected abstract Attack ConstructAttack(Damage damage, float duration, float delay, LayerMask layerMask);
    }
}