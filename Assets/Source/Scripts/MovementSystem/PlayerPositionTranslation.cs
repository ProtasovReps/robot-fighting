using FiniteStateMachine;
using FiniteStateMachine.Conditions;
using InputSystem;
using Reflex.Attributes;
using UnityEngine;

namespace MovementSystem
{
    public class PlayerPositionTranslation : PositionTranslation
    {
        [SerializeField] private PlayerInputReader _playerInputReader;
        
        [Inject]
        private void Inject(PlayerStateMachine stateMachine, PlayerConditionBuilder conditionAddable)
        {
            Initialize(stateMachine, conditionAddable, _playerInputReader);
        }
    }
}