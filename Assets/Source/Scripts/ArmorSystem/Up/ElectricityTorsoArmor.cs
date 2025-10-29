using HitSystem.FighterParts;

namespace ArmorSystem.Up
{
    public class ElectricityTorsoArmor : ElectricityArmor<Torso>
    {
        public ElectricityTorsoArmor(Torso fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}