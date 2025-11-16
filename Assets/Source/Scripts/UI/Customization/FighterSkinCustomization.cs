using CharacterSystem;
using R3;
using UI.Buttons;
using UI.Panel;
using UnityEngine;

namespace UI.Customization
{
    public class FighterSkinCustomization : MonoBehaviour
    {
        [SerializeField] private UnitButton _nextButton;
        [SerializeField] private UnitButton _previousButton;
        [SerializeField] private FighterShowcase _fighterShowcase;
        [SerializeField] private SetupSkinPanel _setupSkinPanel;
        [SerializeField] private BuySkinPanel _buySkinPanel;
        
        public void Initialize(Wallet wallet)
        {
            _setupSkinPanel.Initialize(_fighterShowcase);
            _buySkinPanel.Initialize(_fighterShowcase, wallet);
            _fighterShowcase.Initialize();
            
            _nextButton.Pressed
                .Subscribe(_ => _fighterShowcase.ShowNext())
                .AddTo(this);

            _previousButton.Pressed
                .Subscribe(_ => _fighterShowcase.ShowPrevious())
                .AddTo(this);
        }
    }
}