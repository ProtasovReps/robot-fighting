using Interface;
using Reflex.Attributes;

namespace FightingSystem
{
    public class PlayerAttacker : Attacker
    {
        [Inject]
        private void Inject(IPlayerStateMachine stateMachine, IPlayerConditionAddable conditionAddable)
        {
            SubscribeStateMachine(stateMachine, conditionAddable);
        }
    }
}