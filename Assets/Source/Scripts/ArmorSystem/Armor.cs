using System;
using FightingSystem;
using FightingSystem.AttackDamage;
using HitSystem.FighterParts;
using Interface;
using UnityEngine;

namespace ArmorSystem
{
    public abstract class Armor<TFighterPart> : IDamageable<Damage>
        where TFighterPart : DamageableFighterPart
    {
        private const float NotMatchedTypeReduceValue = 2f;
        
        public Armor(TFighterPart fighterPart, float damageReduceAmount)
        {
            if (fighterPart == null)
                throw new ArgumentNullException(nameof(fighterPart));

            if (damageReduceAmount < 0)
                throw new ArgumentOutOfRangeException(nameof(damageReduceAmount));
                
            DamageReduceAmount = damageReduceAmount;
            FighterPart = fighterPart;
        }
        
        protected float DamageReduceAmount { get; }
        protected TFighterPart FighterPart { get; }

        public void AcceptDamage(Damage damage)
        {
            float newDamageValue = damage.Value - DamageReduceAmount / NotMatchedTypeReduceValue;

            if (IsValidDamageType(damage.Type))
                newDamageValue = damage.Value - DamageReduceAmount;
            
            newDamageValue = Mathf.Clamp(newDamageValue, 0, newDamageValue);
            damage = new Damage(newDamageValue, damage.ImpulseForce, damage.Type);
            
            FighterPart.AcceptDamage(damage);
        }

        protected abstract bool IsValidDamageType(DamageType damageType);
    }
}