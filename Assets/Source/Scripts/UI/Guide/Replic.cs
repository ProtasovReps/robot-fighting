using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UI.Buttons;
using UnityEngine;
using Unit = R3.Unit;

namespace UI.Guide
{
    public class Replic : MonoBehaviour
    {
        private readonly Subject<Unit> _executed = new();

        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration;
        [SerializeField] private UnitButton _nextButton;

        private CancellationTokenSource _tokenSource;
        private bool _isPlaying; //temp, после проверять animatable на executed;

        public Observable<Unit> Executed => _executed;

        private void Awake()
        {
            _nextButton.Pressed
                .Subscribe(_ => Cancel())
                .AddTo(this);
        }

        public void Say()
        {
            if (_tokenSource != null)
                _tokenSource.Cancel();

            _tokenSource = new CancellationTokenSource();

            Interpolate().Forget();
        }

        private async UniTaskVoid Interpolate()
        {
            float elapsedTime = 0f;
            int maxCharacters = _text.text.Length;

            _text.maxVisibleCharacters = 0;

            while (elapsedTime < _duration && _tokenSource.IsCancellationRequested == false)
            {
                float newMaxCharacters = Mathf.Lerp(0, maxCharacters, elapsedTime / _duration);

                _text.maxVisibleCharacters = (int)newMaxCharacters;
                elapsedTime += Time.unscaledDeltaTime;
                await UniTask.Yield(cancellationToken: _tokenSource.Token, cancelImmediately: true);
            }

            Cancel();
        }

        private void Cancel()
        {
            if (_tokenSource.IsCancellationRequested == false)
            {
                _tokenSource.Cancel();
                _text.maxVisibleCharacters = _text.text.Length;
            }
            else
            {
                _executed.OnNext(Unit.Default);
            }
        }
    }
}