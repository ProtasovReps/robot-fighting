using UnityEngine;

namespace CharacterSystem.Data
{
    public class BotData : FighterData
    {
        [field: SerializeField] [field: Min(1)] public float MoveDuration { get; private set; }
        [field: SerializeField] [field: Min(1)] public float AttackDelay { get; private set; }
    }
}