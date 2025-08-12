using System;
using Interface;
using UnityEngine;
using R3;

namespace AnimationSystem
{
    public abstract class CharacterAnimation : IDisposable
    {
        private readonly IStateMachine _stateMachine;

        private IDisposable _subscription;

        protected CharacterAnimation(IStateMachine stateMachine, Animator animator)
        {
            if (stateMachine == null)
                throw new ArgumentNullException(nameof(stateMachine));

            if (animator == null)
                throw new ArgumentNullException(nameof(animator));

            _stateMachine = stateMachine;
            Animator = animator;
        }

        protected Animator Animator { get; }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        public void Subscribe()
        {
            _subscription = _stateMachine.Value
                .Where(state => state.Type == GetRequiredState())
                .Subscribe(_ => Animate());
        }

        protected abstract Type GetRequiredState();

        protected abstract void Animate();
    }
}