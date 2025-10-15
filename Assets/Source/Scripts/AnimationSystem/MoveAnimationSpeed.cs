using System;
using Extensions;
using MovementSystem;
using UnityEngine;
using R3;

namespace AnimationSystem
{
    public class MoveAnimationSpeed : IDisposable
    {
        private const float MaxAnimatorSpeed = 1.5f;

        private readonly Animator _animator;
        private readonly IDisposable _subscription;
        private readonly float _maxMoveSpeed;

        public MoveAnimationSpeed(Animator animator, PositionTranslation positionTranslation,
            float maxMoveSpeed)
        {
            _animator = animator;
            _maxMoveSpeed = maxMoveSpeed;
            _subscription = positionTranslation.CurrentSpeed
                .Subscribe(SetSpeed);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        private void SetSpeed(float speed)
        {
            float mappedSpeed = speed.Remap(0, _maxMoveSpeed, 0, MaxAnimatorSpeed);

            _animator.SetFloat(MotionHashes.WalkSpeed, mappedSpeed);
        }
    }
}