using HitSystem.FighterParts;

namespace ArmorSystem.Up
{
    public class FireTorsoArmor : FireArmor<Torso>
    {
        public FireTorsoArmor(Torso fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}