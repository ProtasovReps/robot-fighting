using System;
using System.Collections.Generic;
using FiniteStateMachine.States;
using UnityEngine;

namespace AnimationSystem
{
    public class AnimationStateMapper : MonoBehaviour
    {
        private readonly Dictionary<string, Type> _names = new();
        
        [SerializeField] private AnimationClip[] _upAttackAnimations;
        [SerializeField] private AnimationClip[] _downAttackAnimations;
        [SerializeField] private AnimationClip[] _superAttackAnimations;

        public void Initialize()
        {
            Add(_upAttackAnimations, typeof(UpAttackState));
            Add(_downAttackAnimations, typeof(DownAttackState));
            Add(_superAttackAnimations, typeof(SuperAttackState));
        }

        public Type Get(string key)
        {
            if (_names.ContainsKey(key) == false)
                throw new KeyNotFoundException(key);

            return _names[key];
        }

        public bool Contains(string key)
        {
            return _names.ContainsKey(key);
        }
        
        private void Add(AnimationClip[] clips, Type state)
        {
            for (int i = 0; i < clips.Length; i++)
            {
                if (_names.ContainsKey(clips[i].name))
                    throw new ArgumentOutOfRangeException(clips[i].name);

                _names.Add(clips[i].name, state);
            }
        }
    }
}