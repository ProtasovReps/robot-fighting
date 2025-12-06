using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using Interface;
using R3;
using Reflex.Attributes;
using TMPro;
using UnityEngine;

namespace UI.Effect
{
    public class NotEnoughMoneyEffect : MonoBehaviour
    {
        [SerializeField] private ScaleAnimation _scaleAnimation;
        [SerializeField] private TMP_Text _moneyAmount;
        [SerializeField] private Color _targetColor;
        [SerializeField] private SoundID _errorSound;
        
        private Color _defaultColor;
        
        [Inject]
        private void Inject(IMoneySpendable moneySpendable)
        {
            _defaultColor = _moneyAmount.color;

            moneySpendable.FailedSpend
                .Subscribe(_ => StartEffect().Forget())
                .AddTo(this);
        }

        private async UniTaskVoid StartEffect()
        {
            if (_scaleAnimation.IsPlaying)
            {
                return;
            }

            BroAudio.Play(_errorSound);
            
            _moneyAmount.color = _targetColor;
            await _scaleAnimation.Play();
            _moneyAmount.color = _defaultColor;
        }
    }
}