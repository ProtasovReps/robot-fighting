using System;
using Interface;
using UnityEngine;

namespace AnimationSystem
{
    public class AnimatedCharacter : MonoBehaviour
    {
        private IAnimation[] _animations;

        [field: SerializeField] public Animator Animator { get; private set; }

        private void OnDestroy()
        {
            if (_animations == null)
            {
                return;
            }

            for (int i = 0; i < _animations.Length; i++)
            {
               _animations[i].Dispose();
            }
        }

        public void Initialize(IAnimation[] animations)
        {
            if (animations == null)
            {
                throw new ArgumentNullException(nameof(animations));
            }

            if (animations.Length == 0)
            {
                throw new ArgumentOutOfRangeException(nameof(animations));
            }

            _animations = animations;

            for (int i = 0; i < _animations.Length; i++)
            {
                _animations[i].Subscribe();
            }
        }
    }
}