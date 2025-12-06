using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using Reflex.Attributes;

namespace FightingSystem.Attackers
{
    public class BotAttacker : Attacker
    {
        [Inject]
        private void Inject(BotStateMachine stateMachine, BotConditionBuilder conditionAddable)
        {
            Subscribe(stateMachine, conditionAddable);
        }
    }
}