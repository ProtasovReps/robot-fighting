using UnityEngine;

namespace CharacterSystem.Parameters
{
    public abstract class FighterParameters : MonoBehaviour
    {
        [field: SerializeField] [field: Min(1f)] public float StartHealthValue { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float StunDuration { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float DownStunDuration { get; private set; }
        [field: SerializeField] [field: Min(0.5f)] public float BlockDuration { get; private set; }
        [field: SerializeField] [field: Min(1f)] public float BlockValue { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float MoveSpeed { get; private set; }
    }
}