using UI.Store;
using UnityEngine;

namespace UI.Buttons
{
    public class BuyObservableButton : ObservableButton<BuyGoodPanel>
    {
        [SerializeField] private BuyGoodPanel _buyGoodPanel;
        
        protected override BuyGoodPanel GetReturnable()
        {
            return _buyGoodPanel;
        }
    }
}