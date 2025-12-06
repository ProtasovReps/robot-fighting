using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using TMPro;
using UI.Buttons;
using UI.Effect;
using UnityEngine;
using Unit = R3.Unit;

namespace UI.Guide
{
    public class Replic : Animatable
    {
        private readonly Subject<Unit> _executed = new();

        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _duration;
        [SerializeField] private UnitButton _nextButton;

        private float _maxCharacters;
        private bool _isSkipped;
        
        public Observable<Unit> Executed => _executed;
        protected Subject<Unit> SubjectExecuted => _executed;

        private void Awake()
        {
            _nextButton.Pressed
                .Subscribe(_ => Cancel())
                .AddTo(this);
        }

        public virtual void Say()
        {
            Cancel();

            _maxCharacters = _text.text.Length;
            _text.maxVisibleCharacters = 0;
            _isSkipped = false;
            
            Play().Forget();
        }

        protected override void Animate(float factor)
        {
            _text.maxVisibleCharacters = (int)Mathf.Lerp(0, _maxCharacters, factor);
        }

        protected override void Cancel()
        {
            if (_isSkipped == false)
            {
                base.Cancel();
                _isSkipped = true;
                _text.maxVisibleCharacters = _text.text.Length;
            }
            else
            {
                _executed.OnNext(Unit.Default);
            }
        }
    }
}