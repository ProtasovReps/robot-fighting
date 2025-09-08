using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants
{
    public class SpecialLegMeleeAttackImplant : MeleeAttackImplant
    {
        public override Type RequiredState => typeof(SpecialAttackState);
        public override AttackPart RequiredPart => AttackPart.Legs;
    }
}