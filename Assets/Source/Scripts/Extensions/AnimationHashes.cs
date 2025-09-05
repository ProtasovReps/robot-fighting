using UnityEngine;

namespace Extensions
{
    public static class AnimationHashes
    {
        public static readonly int Idle = Animator.StringToHash(nameof(Idle));
        public static readonly int MoveLeft = Animator.StringToHash(nameof(MoveLeft));
        public static readonly int MoveRight = Animator.StringToHash(nameof(MoveRight));
        public static readonly int Jump = Animator.StringToHash(nameof(Jump));
        public static readonly int ArmAttack = Animator.StringToHash(nameof(ArmAttack));
        public static readonly int LegAttack = Animator.StringToHash(nameof(LegAttack));
        public static readonly int HeadHit = Animator.StringToHash(nameof(HeadHit));
        public static readonly int LegsHit = Animator.StringToHash(nameof(LegsHit));
        public static readonly int Block = Animator.StringToHash(nameof(Block));
        public static readonly int WalkSpeed = Animator.StringToHash(nameof(WalkSpeed));
        public static readonly int Special = Animator.StringToHash(nameof(Special));
    }
}