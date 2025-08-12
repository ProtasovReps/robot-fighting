using System;
using UnityEngine;

namespace MovementSystem
{
    public class PositionTranslation
    {
        private const int MinDirection = -1;
        private const int MaxDirection = 1;
        
        private readonly Transform _transform;
        private readonly float _moveSpeed;
        
        public PositionTranslation(Transform transform, float moveSpeed)
        {
            if (transform == null)
                throw new ArgumentNullException(nameof(transform));
            
            if (moveSpeed <= 0)
                throw new ArgumentOutOfRangeException(nameof(moveSpeed));

            _transform = transform;
            _moveSpeed = moveSpeed;
        }

        public void TranslatePosition(int direction)
        {
            direction = Mathf.Clamp(direction, MinDirection, MaxDirection);
            
            float targetDistance = direction * _moveSpeed * Time.deltaTime;
            Vector3 targetPosition = _transform.position + Vector3.right * targetDistance;

            _transform.position = targetPosition;
        }
    }
}