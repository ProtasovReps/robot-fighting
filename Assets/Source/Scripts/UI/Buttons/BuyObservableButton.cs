using UI.Panel;
using UnityEngine;

namespace UI.Buttons
{
    public class BuyObservableButton : ObservableButton<GoodPanel>
    {
        [SerializeField] private GoodPanel _goodPanel;
        
        protected override GoodPanel GetReturnable()
        {
            return _goodPanel;
        }
    }
}