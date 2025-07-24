using InputSystem;
using Interface;
using Reflex.Attributes;
using UnityEngine;

namespace MovementSystem
{
    public class BotPositionTranslation : PositionTranslation
    {
        [SerializeField] private BotInputReader _botInputReader;
        
        [Inject]
        private void Inject(IBotStateMachine stateMachine, IBotConditionAddable conditionAddable)
        {
            Initialize(stateMachine, conditionAddable, _botInputReader);
        }
    }
}