using HitSystem.FighterParts;

namespace ArmorSystem.Up
{
    public class PlasmaTorsoArmor : PlasmaArmor<Torso>
    {
        public PlasmaTorsoArmor(Torso fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}