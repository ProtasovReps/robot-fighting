using UnityEngine;

namespace FightingSystem.Attacks
{
    [CreateAssetMenu(fileName = "DamageData", menuName = "Fighter/DamageData")]
    public class DamageData : ScriptableObject
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public DamageType Type { get; private set; }
        [field: SerializeField] public float ImpulseForce { get; private set; }
    }
}