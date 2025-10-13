using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants.Ranged
{
    public class LegRangedAttackImplant : RangedAttackImplant
    {
        public override Type RequiredState => typeof(DownAttackState);
        public override AttackPart RequiredPart => AttackPart.Legs;
    }
}