using UnityEngine;

namespace UI.Switchers
{
    public class CanvasGroupSwitcher : MonoBehaviour
    {
        [SerializeField] private float _enabledTransparency;
        [SerializeField] private float _disabledTransparency;
        [SerializeField] private CanvasGroup _group;
        
        protected void Enable()
        {
            _group.interactable = true;
            _group.alpha = _enabledTransparency;
        }

        protected void Disable()
        {
            
            _group.interactable = false;
            _group.alpha = _disabledTransparency;
        }
    }
}