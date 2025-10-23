using ImplantSystem.AttackImplants;
using UnityEngine;

namespace ImplantSystem.Factory
{
    public class BossImplantFactory : BotImplantFactory
    {
        [SerializeField] private AttackImplant _superAttackImplant;

        protected override AttackImplant[] GetImplants()
        {
            AddImplant(_superAttackImplant);
            return base.GetImplants();
        }
    }
}