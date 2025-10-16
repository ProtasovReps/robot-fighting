using System;
using FightingSystem.AttackDamage;
using HitSystem.FighterParts;
using Interface;

namespace ArmorSystem
{
    public abstract class Armor<TFighterPart> : IDamageable<Damage>
        where TFighterPart : DamageableFighterPart
    {
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
        
        public abstract void AcceptDamage(Damage damage);
    }
}