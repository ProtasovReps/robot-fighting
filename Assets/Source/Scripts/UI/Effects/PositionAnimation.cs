using UnityEngine;

namespace UI.Effect
{
    public class PositionAnimation : Animatable
    {
        [SerializeField] private Transform _target;
        
        private Transform _transform;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        private bool _isFirstLoop = true;
        
        private void Awake()
        {
            _transform = transform;
        }

        protected override void Animate(float factor)
        {
            if (_isFirstLoop)
            {
                _startPosition = _transform.position;
                _targetPosition = _target.position;
                _isFirstLoop = false;
            }
            
            _transform.position = Vector3.Lerp(_startPosition, _targetPosition, factor);
        }
    }
}