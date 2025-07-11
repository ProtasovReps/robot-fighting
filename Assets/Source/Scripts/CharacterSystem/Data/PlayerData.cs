using InputSystem;
using MovementSystem;
using UnityEngine;

namespace CharacterSystem.Data
{
    public class PlayerData : FighterData
    {
        [field: SerializeField] public PlayerInputReader PlayerInputReader { get; private set; }
        [field: SerializeField] public PlayerPositionTranslation PositionTranslation { get; private set; }
        [field: SerializeField] public Jump Jump { get; private set; }
    }
}