using AnimationSystem;
using HitSystem;
using UnityEngine;

namespace CharacterSystem.Parameters
{
    public class SkinData : MonoBehaviour
    {
        [field: SerializeField] public HitEffectStash HitEffectStash { get; private set; } // убрать
        [field: SerializeField] public AnimatedCharacter AnimatedCharacter { get; private set; } // убрать
    }
}