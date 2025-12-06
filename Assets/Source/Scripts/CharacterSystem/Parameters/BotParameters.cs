using UnityEngine;

namespace CharacterSystem.Parameters
{
    public class BotParameters : FighterParameters
    {
        [field: SerializeField, Min(0.1f)] public float MoveDuration { get; private set; }
        [field: SerializeField, Min(0.1f)] public float IdleDuration { get; private set; }
        [field: SerializeField, Min(0f)] public float BaseDamage { get; private set; }
        [field: SerializeField, Min(0f)] public float AttackInputDelay { get; private set; }
        [field: SerializeField, Min(0f)] public float BaseImpulse { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float MoveSpeed { get; private set; }
        [field: SerializeField] [field: Min(1f)] public float StartHealthValue { get; private set; }
        [field: SerializeField] [field: Min(1f)] public float BlockValue { get; private set; }
    }
}