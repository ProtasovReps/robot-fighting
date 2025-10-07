using UnityEngine;

namespace CharacterSystem.Data
{
    public abstract class FighterData : MonoBehaviour
    {
        [field: SerializeField] [field: Min(1f)] public float StartHealthValue { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float StunDuration { get; private set; }
        [field: SerializeField] [field: Min(0.1f)] public float DownStunDuration { get; private set; }
        [field: SerializeField] [field: Min(1f)] public float BlockDuration { get; private set; }
        [field: SerializeField] [field: Min(1f)] public float BlockValue { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public SkinData SkinData { get; private set; }
    }
}