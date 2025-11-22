using R3;
using TMPro;
using UI.Buttons;
using UI.Panel;
using UnityEngine;

namespace UI.LevelEnd
{
    public class MultiplyAwardPanel : SwitchablePanel
    {
        [SerializeField] private UnitButton _button;
        [SerializeField] private TMP_Text _multipliedAwardText;
        [SerializeField] private Chest _chest;
        [SerializeField, Min(1)] private int _multiplier;
        
        private void Awake()
        {
            _multipliedAwardText.text = (_chest.AwardAmount * _multiplier).ToString();

            _button.Pressed
                .Subscribe(_ => Multiply())
                .AddTo(this);
        }

        private void Multiply()
        {
            // показать рекламу

            int addAmount = _multiplier - 1;

            for (int i = 0; i < addAmount; i++)
                _chest.AddAward();

            SetEnable(false);
        }
    }
}