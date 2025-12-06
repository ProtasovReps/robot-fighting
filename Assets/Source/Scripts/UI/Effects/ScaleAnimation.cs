using UnityEngine;

namespace UI.Effect
{
    public class ScaleAnimation : Animatable
    {
        [SerializeField] private AnimationCurve _curve;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        protected override void Animate(float factor)
        {
            float newScale = _curve.Evaluate(factor);
            
            _transform.localScale = new Vector2(newScale, newScale);
        }
    }
}