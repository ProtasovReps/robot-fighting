using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface;
using R3;
using TMPro;
using UnityEngine;

namespace UI.Info
{
    public class NotEnoughMoneyEffect : MonoBehaviour
    {
        private const int AnimationsCount = 2;
        
        [SerializeField] private TMP_Text _moneyAmount;
        [SerializeField] private float _targetScale;
        [SerializeField] private float _duration;
        [SerializeField] private Color _targetColor;

        private Color _defaultColor;
        private Vector3 _defaultScale;
        private CancellationTokenSource _tokenSource;

        private void OnDestroy()
        {
            _tokenSource?.Cancel();
        }

        public void Initialize(IMoneySpendable moneySpendable)
        {
            _defaultColor = _moneyAmount.color;
            _defaultScale = _moneyAmount.transform.localScale;

            moneySpendable.FailedSpend
                .Subscribe(_ => StartAnimation().Forget())
                .AddTo(this);
        }

        private async UniTaskVoid StartAnimation()
        {
            if (_tokenSource?.IsCancellationRequested == false)
                return;
            
            _tokenSource = new CancellationTokenSource();
            _moneyAmount.color = _targetColor;

            await Animate(_tokenSource.Token, _defaultScale.x, _targetScale);
            await Animate(_tokenSource.Token, _targetScale, _defaultScale.x);

            _moneyAmount.color = _defaultColor;
            _tokenSource?.Cancel();
        }
        
        private async UniTask Animate(CancellationToken token, float startScale, float targetScale)
        {
            float expired = 0f;
            float duration = _duration / AnimationsCount;
            Transform animatable = _moneyAmount.transform;
            
            while (expired < duration && token.IsCancellationRequested == false)
            {
                float newScale = Mathf.Lerp(startScale, targetScale, expired / duration);
                
                animatable.localScale = new Vector2(newScale, newScale);
                expired += Time.deltaTime;
                await UniTask.Yield(cancellationToken: token, cancelImmediately: true);
            }
        }
    }
}