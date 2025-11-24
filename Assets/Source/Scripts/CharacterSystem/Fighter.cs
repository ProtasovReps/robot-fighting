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
        public ImplantPlaceHolderStash ImplantPlaceHolderStash { get; private set; }        
        public HitColliderStash HitColliderStash { get; private set; }        
        public HitEffectStash HitEffectStash { get; private set; }        
        public AnimatedCharacter AnimatedCharacter { get; private set; }        
        
        public void Initialize()
        {
            ImplantPlaceHolderStash = GetComponent<ImplantPlaceHolderStash>();
            HitColliderStash = GetComponent<HitColliderStash>();
            HitEffectStash = GetComponent<HitEffectStash>();
            AnimatedCharacter = GetComponent<AnimatedCharacter>();
        }
    }
}