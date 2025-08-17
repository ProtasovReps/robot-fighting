using System;
using System.Collections.Generic;
using CharacterSystem.Data;
using FiniteStateMachine.States;
using Interface;
using UnityEngine;
using R3;

namespace AnimationSystem
{
    public class AnimationDurationChanger : IDisposable
    {
        private const float DefaultAnimatorSpeed = 1f;

        private readonly Dictionary<Type, float> _stateDurations;
        private readonly Animator _animator;
        private readonly IDisposable _subscription;

        public AnimationDurationChanger(
            Animator animator,
            IStateMachine stateMachine,
            FighterData fighterData)
        {
            _stateDurations = new Dictionary<Type, float>
            {
                { typeof(IdleState), DefaultAnimatorSpeed },
                { typeof(UpHittedState), fighterData.StunDuration },
                { typeof(DownHittedState), fighterData.DownStunDuration },
                // а также скорость передвижения, ударов и тд
            };

            _animator = animator;
            _subscription = stateMachine.Value
                .Subscribe(SetSpeed);
        }

        public void Dispose()
        {
            _subscription?.Dispose();
        }
 
        private void SetSpeed(State state)
        {
            Type stateType = state.Type;

            if (_stateDurations.TryGetValue(stateType, out float duration) == false)
            {
                _animator.speed = DefaultAnimatorSpeed;    
                return;
            }

            _animator.speed = DefaultAnimatorSpeed / duration;
        }
    }
}