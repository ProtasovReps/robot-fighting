using UnityEngine;

namespace Extensions
{
    public static class AnimationHashes
    {
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));
        public static readonly int ForwardMove = Animator.StringToHash(nameof(ForwardMove));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}