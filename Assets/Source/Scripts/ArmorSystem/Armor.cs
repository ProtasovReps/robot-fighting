using System;
using FightingSystem.AttackDamage;
using HitSystem.FighterParts;
using Interface;
using UnityEngine;

namespace ArmorSystem
{
    public abstract class Armor<TFighterPart> : MonoBehaviour, IDamageable<Damage>
        where TFighterPart : DamageableFighterPart
    {
        [field: SerializeField] protected float DamageReduceAmount;

        protected TFighterPart FighterPart { get; private set; }

        public void Initialize(TFighterPart fighterPart)
        {
            if (fighterPart == null)
                throw new ArgumentNullException(nameof(fighterPart));
            
            FighterPart = fighterPart;
        }
        
        public abstract void AcceptDamage(Damage damage);
    }
}