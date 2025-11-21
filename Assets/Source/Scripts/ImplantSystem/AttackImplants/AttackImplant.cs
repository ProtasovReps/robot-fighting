using FightingSystem;
using FightingSystem.AttackDamage;
using FightingSystem.Attacks;
using UnityEngine;

namespace ImplantSystem.AttackImplants
{
    public abstract class AttackImplant : MonoBehaviour
    {
        [SerializeField] private DamageFactory _damageFactory;
        
        [field: SerializeField] public AttackParameters Parameters { get; private set; }

        public Attack GetAttack(LayerMask layer, Damage baseDamage)
        {
            Damage damage = _damageFactory.Produce(baseDamage);
            return Construct(damage, layer);
        }

        protected abstract Attack Construct(Damage damage, LayerMask layer);
    }
}