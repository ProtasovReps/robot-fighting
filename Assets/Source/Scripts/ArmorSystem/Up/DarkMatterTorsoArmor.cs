using HitSystem.FighterParts;

namespace ArmorSystem.Up
{
    public class DarkMatterTorsoArmor : DarkMatterArmor<Torso>
    {
        public DarkMatterTorsoArmor(Torso fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}