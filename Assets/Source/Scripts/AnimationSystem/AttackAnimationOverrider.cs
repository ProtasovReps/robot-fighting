using System;
using System.Collections.Generic;
using Extensions;
using Extensions.Exceptions;
using FiniteStateMachine.States;
using ImplantSystem;
using ImplantSystem.AttackImplants;
using ImplantSystem.PlaceHolders;
using UnityEngine;

namespace AnimationSystem
{
    public class AttackAnimationOverrider
    {
        private readonly HashSet<Type> _requiredAttackTypes;
        private readonly Animator _animator;
        private readonly AnimationStateMapper _stateMapper;
        
        public AttackAnimationOverrider(Animator animator, AnimationStateMapper stateMapper)
        {
            if (animator == null)
            {
                throw new ArgumentNullException(nameof(animator));
            }

            if (stateMapper == null)
            {
                throw new ArgumentNullException(nameof(stateMapper));
            }
            
            _animator = animator;
            _stateMapper = stateMapper;
            
            _requiredAttackTypes = new HashSet<Type>
            {
                typeof(UpAttackState),
                typeof(DownAttackState),
                typeof(SuperAttackState),
            };
        }

        public void Override(ImplantPlaceHolderStash placeHolderStash)
        {
            AnimatorOverrideController overrideController = new (_animator.runtimeAnimatorController);

            foreach (ImplantPlaceHolder placeHolder in placeHolderStash.ActivePlaceHolders)
            {
                foreach (AttackImplant newImplant in placeHolder.Implants)
                {
                    Type attackState = AttackStateComparer.GetAttackState(newImplant.Parameters.RequiredState);

                    if (_requiredAttackTypes.Contains(attackState) == false)
                    {
                        continue;
                    }

                    int animationID = GetAnimationID(overrideController, attackState);
                    string animationName = overrideController.animationClips[animationID].name;
                    
                    overrideController[animationName] = newImplant.Parameters.Clip;
                }
            }

            _animator.runtimeAnimatorController = overrideController;
        }

        private int GetAnimationID(AnimatorOverrideController overrideController, Type requiredState)
        {
            for (int i = 0; i < overrideController.animationClips.Length; i++)
            {
                string clipName = overrideController.animationClips[i].name;

                if (_stateMapper.Contains(clipName) == false)
                {
                    continue;
                }

                if (_stateMapper.Get(clipName) != requiredState)
                {
                    continue;
                }

                return i;
            }

            throw new StateNotFoundException(nameof(requiredState));
        }
    }
}