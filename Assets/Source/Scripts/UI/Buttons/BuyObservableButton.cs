using UI.Panel;
using UnityEngine;

namespace UI.Buttons
{
    public class BuyObservableButton : ObservableButton<BuyGoodPanel>
    {
        [SerializeField] private BuyGoodPanel _buyGoodPanel;
        
        protected override BuyGoodPanel Get()
        {
            return _buyGoodPanel;
        }
    }
}