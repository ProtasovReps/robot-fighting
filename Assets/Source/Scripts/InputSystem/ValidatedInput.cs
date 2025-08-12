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
        private readonly Dictionary<int, DistanceValidator> _distanceValidators;
        private readonly ReactiveProperty<int> _direction;
        private readonly IDisposable _subscription;

        public ValidatedInput(
            Transform transform,
            IMoveInput moveInput,
            Dictionary<int, DistanceValidator> distanceValidators)
        {
            _transform = transform;
            _distanceValidators = distanceValidators;
            _direction = new ReactiveProperty<int>();

            _subscription = moveInput.Value
                .Subscribe(SetDirection);
        }

        public ReadOnlyReactiveProperty<int> Value => _direction;

        public void Dispose()
        {
            _subscription?.Dispose();
            _direction?.Dispose();
        }

        private void SetDirection(int direction)
        {
            if (direction == 0)
                _direction.OnNext(direction);
            else if (_distanceValidators[direction].IsValidDistance(_transform.position))
                _direction.OnNext(direction);
        }
    }
}