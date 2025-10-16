using FightingSystem.Attacks;
using UnityEngine;

namespace FightingSystem.AttackDamage
{
    public class DamageStats : MonoBehaviour
    {
        [field: SerializeField, Min(1f)] public float Damage { get; private set; }
        [field: SerializeField, Min(1f)] public float ImpulseForce { get; private set; }
        [field: SerializeField] public DamageType DamageType { get; private set; } // из FighterData
    }
}