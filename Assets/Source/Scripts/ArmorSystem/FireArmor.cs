using FightingSystem;
using HitSystem.FighterParts;

namespace ArmorSystem
{
    public abstract class FireArmor<TFighterPart> : Armor<TFighterPart>
        where TFighterPart : DamageableFighterPart
    {
        protected FireArmor(TFighterPart fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }

        protected override bool IsValidDamageType(DamageType damageType)
        {
            return damageType == DamageType.Fire 
                   || damageType == DamageType.Default
                   || damageType == DamageType.Plasma; 
        }
    }
}