using System;
using AnimationSystem;
using UnityEngine;

namespace CharacterSystem
{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private CharacterAnimation[] _animations;

        public Animator Animator => _animator;

        private void Start()
        {
            for (int i = 0; i < _animations.Length; i++)
            {
                _animations[i].Subscribe();
            }
        }

        private void OnDestroy()
        {
            // не совсем очевидно, лучше их в диспозер просто кидать, а сам диспозер раздавать в инсталлере
            for (int i = 0; i < _animations.Length; i++)
            {
                _animations[i].Dispose();
            }
        }

        protected void Initialize(CharacterAnimation[] animations)
        {
            if (animations == null)
                throw new ArgumentNullException(nameof(animations));

            if (animations.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(animations));

            _animations = animations;
        }
    }
}