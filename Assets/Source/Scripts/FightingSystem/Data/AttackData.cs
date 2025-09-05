using UnityEngine;

namespace FightingSystem.Attacks
{
    [CreateAssetMenu(fileName = "AttackData", menuName = "Fighter/AttackData")]
    public class AttackData : ScriptableObject
    {
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}