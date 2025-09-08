using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants
{
    public class HandMeleeAttackImplant : MeleeAttackImplant
    {
        public override Type RequiredState => typeof(UpAttackState);
        public override AttackPart RequiredPart => AttackPart.Hands;
    }
}