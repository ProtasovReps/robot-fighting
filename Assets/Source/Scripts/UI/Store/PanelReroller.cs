using R3;
using UI.Buttons;
using UnityEngine;

namespace UI.Store
{
    public class PanelReroller : MonoBehaviour
    {
        [SerializeField] private UnitButton _button;
        [SerializeField] private BuyGoodPanelInitializer _buyGoodPanelInitializer;

        private void Awake()
        {
            _button.Pressed
                .Subscribe(_ => Reroll())
                .AddTo(this);
            
            _buyGoodPanelInitializer.Initialize();
        }

        private void Reroll()
        {
            // Крутим рекламу, а, как посмотрит, перекручиваем ассортимент
            _buyGoodPanelInitializer.Initialize();
        }
    }
}