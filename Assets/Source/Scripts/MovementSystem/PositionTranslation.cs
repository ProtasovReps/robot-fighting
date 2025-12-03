using System;
using R3;
using UnityEngine;

namespace MovementSystem
{
    public class PositionTranslation
    {
        private readonly Transform _transform;
        private readonly ReactiveProperty<float> _currentSpeed;
        private readonly float _moveSpeed;

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

        public void Translate(float direction)
        {
            float newSpeed = Mathf.Lerp(_currentSpeed.Value, _moveSpeed, _moveSpeed * Time.deltaTime);
            float targetDistance = direction * newSpeed * Time.deltaTime;
            Vector3 targetPosition = _transform.position + Vector3.right * targetDistance;

            _currentSpeed.OnNext(newSpeed);
            _transform.position = targetPosition;
        }

        public void ResetSpeed()
        {
            _currentSpeed.OnNext(0f);
        }
    }
}