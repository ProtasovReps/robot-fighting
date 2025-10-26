using FightingSystem;
using UnityEngine;

namespace CharacterSystem.Parameters
{
    public class BotParameters : FighterParameters
    {
        [field: SerializeField, Min(0.1f)] public float MoveDuration { get; private set; }
        [field: SerializeField, Min(0.1f)] public float IdleDuration { get; private set; }
        [field: SerializeField] public float BaseDamage { get; private set; }
        [field: SerializeField] public float AttackInputDelay { get; private set; }
        [field: SerializeField, Min(0f)] public float BaseImpulse { get; private set; }
        [field: SerializeField] public DamageType DamageType { get; private set; }
    }
}