using HitSystem.FighterParts;

namespace ArmorSystem.Down
{
    public class ElectricityLegsArmor : ElectricityArmor<Legs>
    {
        public ElectricityLegsArmor(Legs fighterPart, float damageReduceAmount)
            : base(fighterPart, damageReduceAmount)
        {
        }
    }
}