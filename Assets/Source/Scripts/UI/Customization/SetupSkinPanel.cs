using R3;
using UI.Buttons;
using UI.Panel;
using UnityEngine;
using YG;

namespace UI.Customization
{
    public class SetupSkinPanel : SwitchablePanel
    {
        [SerializeField] private UnitButton _selectButton;
        
        private SkinView _skinView;
        
        public void Initialize(FighterShowcase fighterShowcase)
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