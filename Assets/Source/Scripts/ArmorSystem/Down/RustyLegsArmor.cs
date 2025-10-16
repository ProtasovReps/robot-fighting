using HitSystem.FighterParts;

namespace ArmorSystem.Down
{
    public class RustyLegsArmor : RustyArmor<Legs>
    {
        public RustyLegsArmor(Legs fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}