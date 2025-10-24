using UnityEngine;

namespace CharacterSystem.Parameters
{
    public class BotParameters : FighterParameters
    {
        [field: SerializeField] [field: Min(0.1f)] public float MoveDuration { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float IdleDuration { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float AttackInputDelay { get; private set; }
    }
}