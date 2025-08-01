using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace MovementSystem
{
    public class BotPositionTranslation : PositionTranslation
    {
        [SerializeField] private BotInputReader _botInputReader;
        
        [Inject]
        private void Inject(BotStateMachine stateMachine, BotConditionBuilder conditionAddable)
        {
            Initialize(stateMachine, conditionAddable, _botInputReader);
        }
    }
}