using FightingSystem;
using HitSystem.FighterParts;

namespace ArmorSystem
{
    public abstract class PlasmaArmor<TFighterPart> : Armor<TFighterPart>
        where TFighterPart : DamageableFighterPart
    {
        protected PlasmaArmor(TFighterPart fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }

        protected override bool IsValidDamageType(DamageType damageType)
        {
            return damageType == DamageType.Plasma 
                   || damageType == DamageType.Default
                   || damageType == DamageType.Electricity;
        }
    }
}