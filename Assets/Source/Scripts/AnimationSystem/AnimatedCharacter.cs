using System;
using UnityEngine;

namespace AnimationSystem
{
    public class AnimatedCharacter : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private CharacterAnimation[] _animations;

        private void OnDestroy()
        {
            if (_animations == null)
                return;
            
            for (int i = 0; i < _animations.Length; i++)
               _animations[i].Dispose();
        }

        public void Initialize(CharacterAnimation[] animations)
        {
            if (animations == null)
                throw new ArgumentNullException(nameof(animations));

            if (animations.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(animations));

            _animations = animations;
            
            for (int i = 0; i < _animations.Length; i++)
                _animations[i].Subscribe();
        }
    }
}