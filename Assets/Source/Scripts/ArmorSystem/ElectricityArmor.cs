using FightingSystem;
using HitSystem.FighterParts;

namespace ArmorSystem
{
    public abstract class ElectricityArmor<TFighterPart> : Armor<TFighterPart>
        where TFighterPart : DamageableFighterPart
    {
        protected ElectricityArmor(TFighterPart fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }

        protected override bool IsValidDamageType(DamageType damageType)
        {
            return damageType == DamageType.Electricity 
                   || damageType == DamageType.Default
                   || damageType == DamageType.Fire;
        }
    }
}