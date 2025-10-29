using HitSystem.FighterParts;

namespace ArmorSystem.Down
{
    public class PlasmaLegsArmor : PlasmaArmor<Legs>
    {
        public PlasmaLegsArmor(Legs fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}