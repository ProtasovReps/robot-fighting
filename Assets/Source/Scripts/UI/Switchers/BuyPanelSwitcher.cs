using R3;
using UI.Store;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Switchers
{
    public class BuyPanelSwitcher : ButtonSwitcher
    {
        [SerializeField] private BuyGoodPanel _buyGoodPanel;
        [SerializeField] private Image _baughtImage;
        
        private void Awake()
        {
            _buyGoodPanel.IsEnableSwitched
                .Subscribe(Switch)
                .AddTo(this);
            
            Switch(true);
        }

        private void Switch(bool isPanelEnabled)
        {
            if (isPanelEnabled)
                Enable();
            else
                Disable();
            
            _baughtImage.enabled = !isPanelEnabled;
        }
    }
}