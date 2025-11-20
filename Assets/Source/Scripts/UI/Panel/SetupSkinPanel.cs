using R3;
using Reflex.Attributes;
using UI.Buttons;
using UI.Customization;
using UnityEngine;
using YG;

namespace UI.Panel
{
    public class SetupSkinPanel : SwitchablePanel
    {
        [SerializeField] private UnitButton _selectButton;
        
        private SkinView _skinView;

        [Inject]
        private void Inject(FighterShowcase fighterShowcase)
        {
            fighterShowcase.SkinChanged
                .Subscribe(SetSkinView)
                .AddTo(this);

            _selectButton.Pressed
                .Subscribe(_ => Choose())
                .AddTo(this);
        }
        
        private void SetSkinView(SkinView skinView)
        {
            if (YG2.saves.SettedFighter == skinView.Fighter)
            {
                SetEnable(false);
                return;
            }
            
            SetEnable(true);
            _skinView = skinView;
        }

        private void Choose()
        {
            YG2.saves.SettedFighter = _skinView.Fighter;
            SetEnable(false);
        }
    }
}