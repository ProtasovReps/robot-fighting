using System;
using Extensions;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants.Melee
{
    public class ArmMeleeSuperAttackImplant : MeleeAttackImplant
    {
        public override Type RequiredState => typeof(SuperAttackState);
        public override AttackPart RequiredPart => AttackPart.Hands;
    }
}