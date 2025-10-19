using UnityEngine;

namespace CharacterSystem.Parameters
{
    public class BotParameters : FighterParameters
    {
        [field: SerializeField] public float MoveDuration { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float AttackDelay { get; private set; }
    }
}