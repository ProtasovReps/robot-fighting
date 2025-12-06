using System;
using System.Collections.Generic;
using Extensions;
using Interface;
using R3;
using UnityEngine;

namespace InputSystem
{
    public class ValidatedInput : IMoveInput, IDisposable
    {
        private readonly Transform _transform;
        private readonly Dictionary<float, DistanceValidator> _distanceValidators;
        private readonly ReactiveProperty<float> _direction;
        private readonly IDisposable _subscription;

        public ValidatedInput(
            Transform transform,
            IMoveInput moveInput,
            Dictionary<float, DistanceValidator> distanceValidators)
        {
            _transform = transform;
            _distanceValidators = distanceValidators;
            _direction = new ReactiveProperty<float>();

            _subscription = moveInput.Value
                .Subscribe(SetDirection);
        }

        public ReadOnlyReactiveProperty<float> Value => _direction;

        public void Dispose()
        {
            _subscription?.Dispose();
        }

        private void SetDirection(float direction)
        {
            if (direction == 0)
            {
                _direction.OnNext(direction);
            }
            else if (_distanceValidators[direction].IsValidDistance(_transform.position))
            {
                _direction.OnNext(direction);
            }
        }
    }
}