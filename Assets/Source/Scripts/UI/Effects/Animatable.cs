using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UI.Effect
{
    public abstract class Animatable : MonoBehaviour
    {
        [SerializeField] private float _delay;
        [SerializeField] private float _duration;
        
        private CancellationTokenSource _tokenSource;
        
        public bool IsPlaying => _tokenSource != null;
        
        private void OnDestroy()
        {
            Cancel();
        }

        public async UniTask Play()
        {
            await UniTask.WaitForSeconds(_delay, true);
            
            _tokenSource = new CancellationTokenSource();

            float expiredTime = 0f;

            while (expiredTime < _duration && _tokenSource.IsCancellationRequested == false)
            {
                float factor = expiredTime / _duration;
                
                Animate(factor);
                
                expiredTime += Time.unscaledDeltaTime;
                await UniTask.Yield(cancellationToken: _tokenSource.Token, cancelImmediately: true);
            }

            Cancel();
        }

        protected abstract void Animate(float factor);
        
        protected virtual void Cancel()
        {
            _tokenSource?.Cancel();
            _tokenSource = null;
        }
    }
}