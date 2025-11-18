using UI.Panel;
using UnityEngine;

namespace UI.Buttons
{
    public class BuyObservableButton : ObservableButton<GoodPanel>
    {
        [SerializeField] private GoodPanel _goodPanel;
        
        protected override GoodPanel Get()
        {
            return _goodPanel;
        }
    }
}