using UnityEngine;

namespace UI.ButtonSwitchers
{
    public class ButtonSwitcher : MonoBehaviour
    {
        [SerializeField] private float _enabledTransparency;
        [SerializeField] private float _disabledTransparency;
        [SerializeField] private CanvasGroup _group;

        public void Enable()
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