using R3;
using Reflex.Attributes;
using UI.Buttons;
using UI.Customization;
using UnityEngine;
using YG.Saver;

namespace UI.Panel
{
    public class SetupSkinPanel : SwitchablePanel
    {
        [SerializeField] private UnitButton _selectButton;
        
        private SkinView _skinView;
        private SkinSaver _skinSaver;
        
        [Inject]
        private void Inject(FighterShowcase fighterShowcase, SkinSaver skinSaver)
        {
            _skinSaver = skinSaver;
            
            fighterShowcase.SkinChanged
                .Subscribe(SetSkinView)
                .AddTo(this);

            _selectButton.Pressed
                .Subscribe(_ => Choose())
                .AddTo(this);
        }
        
        private void SetSkinView(SkinView skinView)
        {
            if (_skinSaver.IsSetted(skinView))
            {
                SetEnable(false);
                return;
            }
            
            SetEnable(true);
            _skinView = skinView;
        }

        private void Choose()
        {
            _skinSaver.Set(_skinView);
            SetEnable(false);
        }
    }
}