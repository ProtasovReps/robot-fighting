using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Effect
{
    public class ScaleAnimation : Animatable
    {
        [SerializeField] private AnimationCurve _curve;
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
            _tokenSource = new CancellationTokenSource();
            
            float expiredTime = 0f;

            while (expiredTime < _duration && _tokenSource.IsCancellationRequested == false)
            {
                float newScale = _curve.Evaluate(expiredTime / _duration);

                _transform.localScale = new Vector2(newScale, newScale);
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