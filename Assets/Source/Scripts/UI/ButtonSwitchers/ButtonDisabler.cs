using UnityEngine;
using UnityEngine.UI;

namespace UI.ButtonSwitchers
{
    public class ButtonDisabler : ButtonSwitcher
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.onClick.AddListener(Disable);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Disable);
        }
    }
}