using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using Reflex.Attributes;

namespace FightingSystem.Attackers
{
    public class PlayerAttacker : Attacker
    {
        [Inject]
        private void Inject(PlayerStateMachine stateMachine, PlayerConditionBuilder conditionAddable)
        {
            Subscribe(stateMachine, conditionAddable);
        }
    }
}