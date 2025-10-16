using HitSystem.FighterParts;

namespace ArmorSystem.Up
{
    public class RustyTorsoArmor : RustyArmor<Torso>
    {
        public RustyTorsoArmor(Torso fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}