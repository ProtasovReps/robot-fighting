using System;
using Interface;
using R3;
using UnityEngine;

namespace AnimationSystem
{
    public abstract class CharacterAnimation : IDisposable
    {
        private readonly IStateMachine _stateMachine;
        private readonly Animator _animator;
        
        private IDisposable _subscription;

        protected CharacterAnimation(IStateMachine stateMachine, Animator animator)
        {
            if (stateMachine == null)
                throw new ArgumentNullException(nameof(stateMachine));

            if (animator == null)
                throw new ArgumentNullException(nameof(animator));

            _stateMachine = stateMachine;
            _animator = animator;
        }

        protected Animator Animator => _animator;

        public void Dispose() // диспозер должен быть
        {
            _subscription.Dispose();
        }

        public void Subscribe()
        {
            _subscription = _stateMachine.CurrentState
                .Where(state => state.Type == GetRequiredState())
                .Subscribe(_ => Animate());
        }

        protected abstract Type GetRequiredState();

        protected abstract void Animate();
    }
}