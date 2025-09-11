using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants
{
    public class LegMeleeSuperAttackImplant : MeleeAttackImplant
    {
        public override Type RequiredState => typeof(SuperAttackState);
        public override AttackPart RequiredPart => AttackPart.Legs;
    }
}