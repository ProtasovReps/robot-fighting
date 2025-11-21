using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace HitSystem
{
    public class HitImpact : MonoBehaviour
    {
        private const int LeftImpactDirection = -1;
        private const int RightImpactDirection = 1;

        [SerializeField] private Transform _bound;
        [SerializeField] private float _impactTime;

        private Transform _transform;
        private CancellationTokenSource _tokenSource;
        private int _impactDirection;

        private void Awake()
        {
            _transform = transform;
            _impactDirection = _transform.position.x < 0f ? LeftImpactDirection : RightImpactDirection;
        }

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }

        public void Initialize(HitReader hitReader)
        {
            _tokenSource = new CancellationTokenSource();

            hitReader.Hitted
                .Subscribe(damage => Impact(damage.ImpulseForce).Forget())
                .AddTo(this);
        }

        private async UniTaskVoid Impact(float impulseForce)
        {
            float boundOffset = Mathf.Abs(_transform.position.x - _bound.transform.position.x);
            float clampedTargetPosition = Mathf.Clamp(impulseForce, 0, boundOffset);
            Vector3 targetPosition = _transform.position + Vector3.right * _impactDirection * clampedTargetPosition;

            float expiredTime = 0f;

            while (expiredTime < _impactTime && _tokenSource.IsCancellationRequested == false)
            {
                float newPosition = Mathf.Lerp(_transform.position.x, targetPosition.x, expiredTime / _impactTime);

                _transform.position = new Vector3(newPosition, _transform.position.y, _transform.position.z);
                expiredTime += Time.deltaTime;
                await UniTask.Yield(cancellationToken: _tokenSource.Token, cancelImmediately: true);
            }
        }
    }
}