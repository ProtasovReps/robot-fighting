using System;
using Interface;
using UnityEngine;

namespace AnimationSystem
{
    public class TriggerAnimation<TState> : CharacterAnimation
        where TState : IState
    {
        private readonly int _animationHash;
        
        public TriggerAnimation(IStateMachine stateMachine, Animator animator, int animationHash)
            : base(stateMachine, animator)
        {
            _animationHash = animationHash;
        }

        protected override Type GetRequiredState()
        {
            return typeof(TState);
        }

        protected override void Animate()
        {
            Animator.SetTrigger(_animationHash);
        }
    }
}