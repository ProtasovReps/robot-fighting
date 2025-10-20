using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants.Ranged
{
    public class HandRangeSuperAttackImplant : RangedAttackImplant
    {
        public override Type RequiredState => typeof(SuperAttackState);
        public override AttackPart RequiredPart => AttackPart.Hands;
    }
}