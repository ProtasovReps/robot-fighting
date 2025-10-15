using System;
using System.Collections.Generic;
using Extensions;
using FiniteStateMachine.States;
using ImplantSystem;
using ImplantSystem.AttackImplants;
using ImplantSystem.PlaceHolders;
using UnityEditor.Animations;
using UnityEngine;

namespace AnimationSystem
{
    public class AttackAnimationOverrider
    {
        private readonly Dictionary<Type, int> _requiredHashes;
        private readonly AnimatorStateMachine _stateMachine;
        private readonly Animator _animator;

        public AttackAnimationOverrider(Animator animator)
        {
            if (animator == null)
                throw new ArgumentNullException(nameof(animator));
            
            _stateMachine = ((AnimatorController)animator.runtimeAnimatorController).layers[0].stateMachine;
            _animator = animator;

            _requiredHashes = new Dictionary<Type, int>
            {
                { typeof(UpAttackState), MotionHashes.ArmAttack },
                { typeof(DownAttackState), MotionHashes.LegAttack },
                { typeof(SuperAttackState), MotionHashes.Super },
            };
        }

        public void Override(ImplantPlaceHolderStash placeHolderStash)
        {
            AnimatorOverrideController overrideController = new(_animator.runtimeAnimatorController);
            
            foreach (ImplantPlaceHolder placeHolder in placeHolderStash.PlaceHolders)
            {
                foreach (AttackImplant implant in placeHolder.Implants)
                {
                    if (_requiredHashes.ContainsKey(implant.RequiredState) == false)
                        continue;
                        
                    int animationID = GetAnimationID(_requiredHashes[implant.RequiredState]);
                    string animationName = overrideController.animationClips[animationID].name;
                    
                    overrideController[animationName] = implant.AnimationClip;
                }
            }

            _animator.runtimeAnimatorController = overrideController;
        }

        private int GetAnimationID(int requiredHash)
        {
            for (int i = 0; i < _stateMachine.states.Length; i++)
            {
                if (_stateMachine.states[i].state.nameHash != requiredHash)
                    continue;

                return i;
            }

            throw new ArgumentException(nameof(requiredHash));
        }
    }
}