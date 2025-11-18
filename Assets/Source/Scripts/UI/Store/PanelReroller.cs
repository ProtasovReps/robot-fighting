using R3;
using UI.Buttons;
using UnityEngine;

namespace UI.Store
{
    public class PanelReroller : MonoBehaviour
    {
        [SerializeField] private UnitButton _button;
        [SerializeField] private BuyGoodPanelInstaller _buyGoodPanelInstaller;

        private void Awake()
        {
            _button.Pressed
                .Subscribe(_ => Reroll())
                .AddTo(this);
            
            _buyGoodPanelInstaller.Initialize();
            _buyGoodPanelInstaller.Randomize();
        }

        private void Reroll()
        {
            // Крутим рекламу, а, как посмотрит, перекручиваем ассортимент
            _buyGoodPanelInstaller.Randomize();
        }
    }
}