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

        public override bool IsPlaying => _tokenSource != null;

        public override async UniTask Animate(Transform animatable)
        {
            _tokenSource = new CancellationTokenSource();
            
            float expiredTime = 0f;

            while (expiredTime < _duration && _tokenSource.IsCancellationRequested == false)
            {
                float newScale = _curve.Evaluate(expiredTime / _duration);

                animatable.localScale = new Vector2(newScale, newScale);
                expiredTime += Time.deltaTime;
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