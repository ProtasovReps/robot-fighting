using ImplantSystem.AttackImplants;

namespace ImplantSystem
{
    public class HandImplantPlaceHolder : ImplantPlaceHolder
    {
        public override bool IsValidImplant(AttackImplant implant)
        {
            return implant is HandAttackImplant;
        }
    }
}