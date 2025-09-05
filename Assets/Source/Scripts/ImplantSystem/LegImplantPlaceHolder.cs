using ImplantSystem.AttackImplants;

namespace ImplantSystem
{
    public class LegImplantPlaceHolder : ImplantPlaceHolder
    {
        public override bool IsValidImplant(AttackImplant implant)
        {
            return implant is LegMeleeAttackImplant;
        }
    }
}