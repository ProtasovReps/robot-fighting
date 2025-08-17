using System;
using R3;
using UnityEngine;

namespace MovementSystem
{
    public class PositionTranslation
    {
        private const int MinDirection = -1;
        private const int MaxDirection = 1;
        
        private readonly Transform _transform;
        private readonly ReactiveProperty<float> _currentSpeed;
        private readonly float _moveSpeed;

        private float _currentDirection;
        
        public PositionTranslation(Transform transform, float moveSpeed)
        {
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));
            
            if (moveSpeed <= 0)
                throw new ArgumentOutOfRangeException(nameof(moveSpeed));

            _transform = transform;
            _moveSpeed = moveSpeed;
            _currentSpeed = new ReactiveProperty<float>();
        }

        public ReadOnlyReactiveProperty<float> CurrentSpeed => _currentSpeed;
        
        public void TranslatePosition(int direction)
        {
            if (Mathf.Approximately(_currentDirection, direction) == false)
                _currentSpeed.OnNext(0f);
                
            float newSpeed = Mathf.Lerp(_currentSpeed.Value, _moveSpeed, _moveSpeed * Time.deltaTime);
            
            _currentSpeed.OnNext(newSpeed);
            _currentDirection = Mathf.Clamp(direction, MinDirection, MaxDirection);
            
            float targetDistance = direction * _currentSpeed.Value * Time.deltaTime;
            Vector3 targetPosition = _transform.position + Vector3.right * targetDistance;

            _transform.position = targetPosition;
        }
    }
}