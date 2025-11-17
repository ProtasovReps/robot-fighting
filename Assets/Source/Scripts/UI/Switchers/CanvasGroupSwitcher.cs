using Ami.BroAudio;
using UnityEngine;

namespace UI.Switchers
{
    public class CanvasGroupSwitcher : MonoBehaviour
    {
        [SerializeField] private float _enabledTransparency;
        [SerializeField] private float _disabledTransparency;
        [SerializeField] private CanvasGroup _group;
        [SerializeField] private SoundID _disableSound;
        
        protected void Enable()
        {
            _group.interactable = true;
            _group.alpha = _enabledTransparency;
        }

        protected void Disable()
        {
            BroAudio.Play(_disableSound);
            
            _group.interactable = false;
            _group.alpha = _disabledTransparency;
        }
    }
}