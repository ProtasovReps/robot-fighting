using InputSystem;
using Interface;
using Reflex.Attributes;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerPositionTranslation : PositionTranslation
    {
        [SerializeField] private PlayerInputReader _playerInputReader;
        
        [Inject]
        private void Inject(IPlayerStateMachine stateMachine, IPlayerConditionAddable conditionAddable)
        {
            Initialize(stateMachine, conditionAddable, _playerInputReader);
        }
    }
}