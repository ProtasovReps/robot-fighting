using System;
using FiniteStateMachine.States;
using Interface;
using R3;
using UnityEngine;

namespace AnimationSystem
{
    public class TriggerAnimation<TState> : IAnimation
        where TState : State
    {
        private readonly IStateMachine _stateMachine;
        private readonly int _animationHash;
        private readonly Animator _animator;
        
        private IDisposable _subscription;
        
        public TriggerAnimation(IStateMachine stateMachine, Animator animator, int animationHash)
        {
            if (stateMachine == null)
            {
                throw new ArgumentNullException(nameof(stateMachine));
            }

            if (animator == null)
            {
                throw new ArgumentNullException(nameof(animator));
            }

            _stateMachine = stateMachine;
            _animator = animator;
            _animationHash = animationHash;
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }

        public void Subscribe()
        {
            _subscription = _stateMachine.Value
                .Where(state => state.Type == typeof(TState))
                .Subscribe(_ => _animator.SetTrigger(_animationHash));
        }
    }
}