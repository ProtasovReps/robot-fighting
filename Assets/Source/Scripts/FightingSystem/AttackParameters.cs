using UnityEngine;

namespace FightingSystem
{
    [CreateAssetMenu(fileName = "AttackParameters", menuName = "Fighter/AttackParameters")]
    public class AttackParameters : ScriptableObject
    {
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public AnimationClip Clip { get; private set; }
    }
}