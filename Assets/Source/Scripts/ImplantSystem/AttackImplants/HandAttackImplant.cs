using System;
using FiniteStateMachine.States;

namespace ImplantSystem.AttackImplants
{
    public abstract class HandAttackImplant : AttackImplant
    {
        public override Type RequiredState => typeof(UpAttackState);
    }
}