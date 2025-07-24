using Interface;
using Reflex.Attributes;

namespace FightingSystem
{
    public class BotAttacker : Attacker
    {
        [Inject]
        private void Inject(IBotStateMachine stateMachine, IBotConditionAddable conditionAddable)
        {
            SubscribeStateMachine(stateMachine, conditionAddable);
        }
    }
}