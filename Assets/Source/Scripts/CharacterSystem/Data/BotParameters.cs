using UnityEngine;

namespace CharacterSystem.Parameters
{
    public class BotParameters : FighterParameters
    {
        [field: SerializeField] public float MoveDuration { get; private set; }
        [field: SerializeField] [field: Min(1)] public float AttackDelay { get; private set; }
    }
}