using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants.Ranged
{
    public class HandRangedAttackImplant : RangedAttackImplant
    {
        public override Type RequiredState => typeof(UpAttackState);
        public override AttackPart RequiredPart => AttackPart.Hands;
    }
}