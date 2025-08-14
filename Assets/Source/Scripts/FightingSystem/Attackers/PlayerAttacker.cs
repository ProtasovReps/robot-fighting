using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using Reflex.Attributes;

namespace FightingSystem
{
    public class PlayerAttacker : Attacker
    {
        [Inject]
        private void Inject(PlayerStateMachine stateMachine, PlayerConditionBuilder conditionAddable)
        {
            SubscribeStateMachine(stateMachine, conditionAddable);
        }
    }
}