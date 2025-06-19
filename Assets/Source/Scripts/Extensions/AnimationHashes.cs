using UnityEngine;

namespace Extensions
{
    public static class AnimationHashes
    {
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));
        public static readonly int Move = Animator.StringToHash(nameof(Move));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}