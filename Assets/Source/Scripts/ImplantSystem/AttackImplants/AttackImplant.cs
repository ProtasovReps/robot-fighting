using FightingSystem;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using Interface;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public abstract class AttackImplant : MonoBehaviour, ISellable
    {
        [SerializeField] private DamageFactory _damageFactory;
        
        [field: SerializeField] public AttackParameters AttackParameters { get; private set; }

        public Attack GetAttack(LayerMask opponentLayerMask, Damage baseDamage)
        {
            Damage damage = _damageFactory.Produce(baseDamage);
            return ConstructAttack(damage, AttackParameters.Duration, AttackParameters.Delay, opponentLayerMask);
        }

        protected abstract Attack ConstructAttack(Damage damage, float duration, float delay, LayerMask layerMask);
    }
}