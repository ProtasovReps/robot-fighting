using FightingSystem;
using HitSystem.FighterParts;

namespace ArmorSystem
{
    public abstract class DarkMatterArmor<TFighterPart> : Armor<TFighterPart>
        where TFighterPart : DamageableFighterPart
    {
        protected DarkMatterArmor(TFighterPart fighterPart, float damageReduceAmount) 
            : base(fighterPart, damageReduceAmount)
        {
        }

        protected override bool IsValidDamageType(DamageType damageType)
        {
            return true;
        }
    }
}