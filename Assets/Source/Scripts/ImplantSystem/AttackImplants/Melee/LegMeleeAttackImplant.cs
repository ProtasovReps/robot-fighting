using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants.Melee
{
    public class LegMeleeAttackImplant : MeleeAttackImplant
    {
        public override Type RequiredState => typeof(DownAttackState);
        public override AttackPart RequiredPart => AttackPart.Legs;
    }
}