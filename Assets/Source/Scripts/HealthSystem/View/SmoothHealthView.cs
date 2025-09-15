using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Reflex.Attributes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace HealthSystem
{
    public class SmoothHealthView<T> : MonoBehaviour
        where T : Health
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private float _animationTime;

        private CancellationTokenSource _tokenSource;
        
        [Inject]
        private void Inject(T health)
        {
            _slider.maxValue = health.Value.CurrentValue;

            health.Value
                .Subscribe(Animate)
                .AddTo(this);
        }

        private void OnDestroy()
        {
            Cancel();
        }

        private void Animate(float newValue)
        {
            Cancel();
            
            _tokenSource = new CancellationTokenSource();
            
            UpdateAnimated(newValue, _tokenSource.Token).Forget();
        }
        
        private async UniTaskVoid UpdateAnimated(float newValue, CancellationToken token)
        {
            float elapsedTime = 0f;
            float startValue = _slider.value;

            while (Mathf.Approximately(_slider.value, newValue) == false && token.IsCancellationRequested == false)
            {
                _slider.value = Mathf.Lerp(startValue, newValue, elapsedTime / _animationTime);
                elapsedTime += Time.deltaTime;
                await UniTask.Yield(cancellationToken: token, cancelImmediately: true);
            }
        }

        private void Cancel()
        {
            _tokenSource?.Cancel();
        }
    }
}