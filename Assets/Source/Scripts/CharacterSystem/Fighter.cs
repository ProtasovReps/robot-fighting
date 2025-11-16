using AnimationSystem;
using HitSystem;
using ImplantSystem;
using UnityEngine;

namespace CharacterSystem
{
    [RequireComponent(typeof(ImplantPlaceHolderStash))]
    [RequireComponent(typeof(HitColliderStash))]
    [RequireComponent(typeof(HitEffectStash))]
    [RequireComponent(typeof(AnimatedCharacter))]
    public class Fighter : MonoBehaviour
    {
    }
}