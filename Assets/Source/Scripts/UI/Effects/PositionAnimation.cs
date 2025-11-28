using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Effect
{
    public class PositionAnimation : Animatable
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _delay;
        [SerializeField] private float _duration;
        
        private CancellationTokenSource _tokenSource;
        private Transform _transform;

        public override bool IsPlaying => _tokenSource != null;

        private void Awake()
        {
            _transform = transform;
        }

        public override async UniTask Play()
        {
            await UniTask.WaitForSeconds(_delay, true);
            
            _tokenSource = new CancellationTokenSource();

            float expiredTime = 0f;
            Vector3 startPosition = _transform.position;
            Vector3 targetPosition = _target.position;

            while (expiredTime < _duration && _tokenSource.IsCancellationRequested == false)
            {
                 Vector3 newPosition = Vector3.Lerp(startPosition, targetPosition, expiredTime / _duration);

                _transform.position = new Vector3(newPosition.x, newPosition.y, newPosition.z);
                expiredTime += Time.unscaledDeltaTime;
                await UniTask.Yield(cancellationToken: _tokenSource.Token, cancelImmediately: true);
            }

            Cancel();
        }

        protected override void Cancel()
        {
            _tokenSource?.Cancel();
            _tokenSource = null;
        }
    }
}