using Extensions;
using UnityEngine;

namespace FightingSystem.Attacks
{
    public class AttackData : MonoBehaviour
    {
        [field: SerializeField] public Spherecaster Spherecaster { get; private set; }
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public AttackType AttackType { get; private set; }
    }
}