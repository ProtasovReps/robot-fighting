using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using Reflex.Attributes;

namespace FightingSystem
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