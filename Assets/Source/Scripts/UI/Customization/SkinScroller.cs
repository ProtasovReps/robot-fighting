using R3;
using Reflex.Attributes;
using UI.Buttons;
using UI.Panel;
using UnityEngine;

namespace UI.Customization
{
    public class SkinScroller : MonoBehaviour
    {
        [SerializeField] private UnitButton _nextButton;
        [SerializeField] private UnitButton _previousButton;
        [SerializeField] private BuySkinPanel _buySkinPanel;

        [Inject]
        private void Inject(FighterShowcase fighterShowcase)
        {
            _nextButton.Pressed
                .Subscribe(_ => fighterShowcase.ShowNext())
                .AddTo(this);

            _previousButton.Pressed
                .Subscribe(_ => fighterShowcase.ShowPrevious())
                .AddTo(this);
        }
    }
}