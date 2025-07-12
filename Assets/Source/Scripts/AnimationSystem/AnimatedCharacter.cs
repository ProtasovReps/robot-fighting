using System;
using UnityEngine;

namespace AnimationSystem
{
    public class AnimatedCharacter : MonoBehaviour
    {
        [field: SerializeField] public Animator Animator { get; private set; }

        private CharacterAnimation[] _animations;

        private void Start()
        {
            for (int i = 0; i < _animations.Length; i++)
            {
                _animations[i].Subscribe();
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < _animations.Length; i++)
            {
               _animations[i].Dispose();
            }
        }

        public void Initialize(CharacterAnimation[] animations)
        {
            if (animations == null)
                throw new ArgumentNullException(nameof(animations));

            if (animations.Length == 0)
                throw new ArgumentOutOfRangeException(nameof(animations));

            _animations = animations;
        }
    }
}