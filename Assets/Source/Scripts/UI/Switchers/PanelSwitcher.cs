using Ami.BroAudio;
using R3;
using UI.Panel;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Switchers
{
    public class PanelSwitcher : CanvasGroupSwitcher
    {
        [SerializeField] private SwitchablePanel _informationalPanel;
        [SerializeField] private Image _disabledImage;
        [SerializeField] private SoundID _disableSound;

        private void Awake()
        {
            _informationalPanel.EnableSwitched
                .Subscribe(Switch)
                .AddTo(this);

            Switch(true);
        }

        private void Switch(bool isPanelEnabled)
        {
            if (isPanelEnabled)
            {
                Enable();
            }
            else
            {
                BroAudio.Play(_disableSound);
                Disable();
            }

            _disabledImage.enabled = !isPanelEnabled;
        }
    }
}