using HitSystem.FighterParts;

namespace ArmorSystem.Down
{
    public class DarkMatterLegsArmor : DarkMatterArmor<Legs>
    {
        public DarkMatterLegsArmor(Legs fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}