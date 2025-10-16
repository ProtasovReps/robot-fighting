using FightingSystem.AttackDamage;
using HitSystem.FighterParts;
using UnityEngine;

namespace ArmorSystem
{
    public abstract class RustyArmor<TFighterPart> : Armor<TFighterPart>
        where TFighterPart : DamageableFighterPart
    {
        protected RustyArmor(TFighterPart fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }

        public override void AcceptDamage(Damage damage)
        {
            float newDamageValue = damage.Value - DamageReduceAmount;
            float clampedDamage = Mathf.Clamp(newDamageValue, 0, newDamageValue);
            
            damage = new Damage(clampedDamage, damage.ImpulseForce, damage.Type);

            FighterPart.AcceptDamage(damage);
        }
    }
}