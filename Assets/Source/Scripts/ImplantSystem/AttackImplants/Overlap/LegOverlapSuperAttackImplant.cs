using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants.Overlap
{
    public class LegOverlapSuperAttackImplant : OverlapAttackImplant
    {
        public override Type RequiredState => typeof(SuperAttackState);
        public override AttackPart RequiredPart => AttackPart.Legs;
    }
}