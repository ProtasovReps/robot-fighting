using Interface;
using Reflex.Attributes;
using UI.Buttons;
using UI.Effect;
using UI.Info;
using UnityEngine;

namespace UI.VictoryMenu
{
    public class ChestPanelInitializer : MonoBehaviour
    {
        [SerializeField] private IntegerView _moneyAmount;
        [SerializeField] private AnimatablePanelSwitcher _switcher;
        [SerializeField] private UnitButton _goNextButton;

        [Inject]
        private void Inject(IValueChangeable<int> wallet)
        {
            _moneyAmount.Initialize(wallet);
            _switcher.Initialize(_goNextButton.Pressed);
        }
    }
}