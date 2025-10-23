using System;
using UnityEngine;

namespace FightingSystem.Guns
{
    public class Boomerang
    {
        private readonly float _maxOneSideFlyTime;
        private readonly Vector3 _direction;

        private float _flyTime;
        private bool _isPivoted;

        public Boomerang(float maxOneSideFlyTime, Vector3 direction)
        {
            if (maxOneSideFlyTime <= 0f)
                throw new ArgumentOutOfRangeException(nameof(maxOneSideFlyTime));

            _maxOneSideFlyTime = maxOneSideFlyTime;
            _direction = direction;
        }

        public void Tick()
        {
            _flyTime += Time.deltaTime;
            
            if (_flyTime >= _maxOneSideFlyTime)
            {
                _isPivoted = !_isPivoted;
                ResetFlyTime();
            }
        }

        public Vector3 GetDirection()
        {
            if (_isPivoted)
                return -_direction;

            return _direction;
        }

        public void Reset()
        {
            ResetFlyTime();
            _isPivoted = false;
        }

        private void ResetFlyTime()
        {
            _flyTime = 0f;
        }
    }
}