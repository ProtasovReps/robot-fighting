using AnimationSystem;
using HitSystem;
using ImplantSystem;
using UnityEngine;

namespace CharacterSystem.Data
{
    public class SkinData : MonoBehaviour
    {
        [field: SerializeField] public HitColliderStash ColliderStash { get; private set; }
        [field: SerializeField] public HitEffectStash HitEffectStash { get; private set; }
        [field: SerializeField] public ImplantPlaceHolderStash PlaceholderStash { get; private set; }
        [field: SerializeField] public AnimatedCharacter AnimatedCharacter { get; private set; }
    }
}