using HitSystem.FighterParts;

namespace ArmorSystem.Down
{
    public class FireLegsArmor : FireArmor<Legs>
    {
        public FireLegsArmor(Legs fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}